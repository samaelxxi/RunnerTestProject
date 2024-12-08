using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;


namespace DesignPatterns
{
    public abstract class Factory<S, T> : ScriptableObject where T : Poolable<T>
    {
        protected Scene _poolScene;

        Dictionary<S, T> _objects = new();
        protected Dictionary<S, IObjectPool<T>> _pools;

        protected void Initialize(Dictionary<S, T> objects, string sceneName = "")
        {
            if (_pools != null)
                return;
            if (string.IsNullOrEmpty(sceneName))
                sceneName = name;

            _objects = objects;
            _pools = new();

            foreach (var obj in _objects)
            {
                S prefab = obj.Key;  // closure
                var pool = new ObjectPool<T>(() => CreateObject(prefab), GetObject, 
                                            ReleaseObject, DestroyObject);
                _pools[obj.Key] = pool;
            }

            _poolScene = SceneManager.GetSceneByName(sceneName);
            if (!_poolScene.IsValid())
                _poolScene = SceneManager.CreateScene(sceneName);
        }

        protected virtual T Get(S key)
        {
            return _pools[key].Get();
        }

        public virtual void ReleaseAll()
        {
            foreach (var obj in _poolScene.GetRootGameObjects())
                ReleaseObject(obj.GetComponent<T>());
        }

        protected virtual T CreateObject(S key)
        {
            var obj = Instantiate(_objects[key]);
            SceneManager.MoveGameObjectToScene(obj.gameObject, _poolScene);
            obj.gameObject.SetActive(false);
            obj.Pool = _pools[key];

            return obj;
        }

        protected virtual void GetObject(T obj)
        {
            obj.gameObject.SetActive(true);
        }

        protected virtual void ReleaseObject(T obj)
        {
            obj.gameObject.SetActive(false);
        }

        protected virtual void DestroyObject(T obj)
        {
            Destroy(obj.gameObject);
        }
    }
}
