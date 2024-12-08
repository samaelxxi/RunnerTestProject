using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;


namespace RunnerGame.SaveSystem
{
    public class PlayerRecordsSaveable : ISaveable
    {
        public string Name => "Records";

        List<PlayerRecordData> _records;


        public IEnumerable<PlayerRecordData> Records()
        {
            foreach (var record in _records)
                yield return record;
        }

        public void AddNewRecord(PlayerRecordData record)
        {
            Debug.Log("Added new record");
            _records.Add(record);
        }

        public void Load(string data)
        {
            _records = JsonConvert.DeserializeObject<List<PlayerRecordData>>(data);
        }

        public void InitializeDefault()
        {
            _records = new();
        }

        public string Save()
        {
            return JsonConvert.SerializeObject(_records);
        }
    }
}