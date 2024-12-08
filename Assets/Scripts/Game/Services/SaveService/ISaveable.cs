using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RunnerGame.SaveSystem
{
    public interface ISaveable
    {
        string Name { get; }

        void Load(string data);
        void InitializeDefault();
        string Save();
    }
}