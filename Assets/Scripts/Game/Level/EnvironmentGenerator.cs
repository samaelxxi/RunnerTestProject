using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RunnerGame.Level
{
    public class EnvironmentGenerator : MonoBehaviour
    {
        float _leftEnvZ, _rightEnvZ;
        Vector3 _leftLastSize, _rightLastSize = Vector3.one;

        Queue<MeshFilter> _leftObjs = new();
        Queue<MeshFilter> _rightObjs = new();

        LevelGenerationSettingsSO _settings;

        public void SetSettings(LevelGenerationSettingsSO settings)
        {
            _settings = settings;
        }

        public void GenerateIfNeeded(float playerPos)
        {
            while (_leftEnvZ - playerPos < _settings.LevelLength)
                GeneratePart(left: true);

            while (_rightEnvZ - playerPos < _settings.LevelLength)
                GeneratePart(left: false);

            RemoveGarbage(playerPos);
        }

        void RemoveGarbage(float playerPos)
        {
            RemoveListGarbage(_leftObjs, playerPos);
            RemoveListGarbage(_rightObjs, playerPos);
        }

        void RemoveListGarbage(Queue<MeshFilter> queue, float playerPos)
        {
            if (queue.Count == 0) return;

            var obj = queue.Peek();
            var size = obj.sharedMesh.bounds.size;
            var pos = obj.gameObject.transform.position.z;

            if (playerPos - _settings.ReferenceObjectRoadOffset > 
                    pos + size.x / 2 + 50)  // extra big offset for shadows
                Destroy(queue.Dequeue());  // TODO make poolable etc I'm tired
        }

        void GeneratePart(bool left)
        {
            var prefab = _settings.EnvironmentObjects.RandomElement();
            var rotation = Quaternion.Euler(0, left ? -90 : 90, 0);
            var obj = Instantiate(prefab, Vector3.zero, rotation, transform);
            var meshFilter = obj.GetComponent<MeshFilter>();
            var bounds = meshFilter.sharedMesh.bounds;

            ref float posZ = ref (left ? ref _leftEnvZ : ref _rightEnvZ);
            ref Vector3 size = ref (left ? ref _leftLastSize : ref _rightLastSize);
            posZ += size.x / 2 + Random.Range(2.0f, 5.0f);
            posZ += bounds.size.x / 2;
            Vector3 pos = new(0, 0, posZ);
            pos.x -= (left ? 1 : -1) * (_settings.RoadSegmentLength * 2 + bounds.size.z / 2 + 2);
            obj.transform.position = pos;
            size = bounds.size;

            var list = left ? _leftObjs : _rightObjs;
            list.Enqueue(meshFilter);
        }
    }
}