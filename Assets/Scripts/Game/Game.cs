using System.Collections;
using System.Collections.Generic;
using DesignPatterns.Singleton;
using UnityEngine;

public class Game : Singleton<Game>
{
    void OnApplicationQuit()
    {
        ServiceLocator.Get<SaveService>().Save();
    }
}
