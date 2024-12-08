using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Linq;

namespace RunnerGame.SaveSystem
    {
    [Attributes.AutoRegisteredService]
    public class SaveService : Services.IRegistrable, Services.IInitializable
    {
        readonly static string SavePath = $"{Application.persistentDataPath}/data.json";

        readonly List<ISaveable> _saveables = new();

        public bool IsLoaded { get; private set; }

        public void Initialize()
        {
            _saveables.Add(new PlayerRecordsSaveable());
            _saveables.Add(new TestSaveable());

            Load();
        }

        public ISaveable GetSaveable(string name)
        {
            return _saveables.Find(saveable => saveable.Name == name);
        }

        public ISaveable GetSaveable<T>() where T: ISaveable
        {
            return _saveables.Find(saveable => saveable.GetType() == typeof(T));
        }

        public async void Load()
        {
            if (File.Exists(SavePath))
            {
                var task = LoadSaveFile(SavePath);
                var data = await task;
                LoadSaveables(data);
            }
            else
            {
                Debug.Log("Initting default save");
                _saveables.ForEach(saveable => saveable.InitializeDefault());
            }
        }

        public void LoadSaveables(string data)
        {
            var json = JsonConvert.DeserializeObject<Dictionary<string, string>>(data);

            foreach (var saveable in _saveables)
            {
                if (json.TryGetValue(saveable.Name, out string value))
                    saveable.Load(value);
                else
                    saveable.InitializeDefault();
            }

            IsLoaded = true;
        }

        public void Save()
        {
            Dictionary<string, string> saveData = new();
            foreach (var saveable in _saveables)
            {
                var name = saveable.Name;
                var data = saveable.Save();

                if (saveData.ContainsKey(name))
                {
                    Debug.LogError($"Save file already contains {name} data!");
                    continue;
                }
                saveData[name] = data;
            }

            using (var stream = new StreamWriter(SavePath))
            {
                var data = JsonConvert.SerializeObject(saveData);
                stream.Write(data);
            }
        }

        async Task<string> LoadSaveFile(string path)
        {
            var data = "";
            using (var stream = new StreamReader(path))
            {
                data = await stream.ReadToEndAsync();
            }

            return data;
        }
    }
}