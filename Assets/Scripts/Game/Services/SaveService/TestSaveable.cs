using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RunnerGame.SaveSystem
{
    public class TestSaveable : ISaveable
    {
        public string Name => "Test";

        int _i;

        public void InitializeDefault()
        {
            _i = 1;
        }

        public void Load(string data)
        {
            _i = int.Parse(data);
        }

        public string Save()
        {
            return _i.ToString();
        }
    }
}