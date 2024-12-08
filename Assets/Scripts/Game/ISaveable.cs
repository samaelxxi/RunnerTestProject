using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveable
{
    string Name { get; }

    void Load(string data);
    void InitializeDefault();
    string Save();
}
