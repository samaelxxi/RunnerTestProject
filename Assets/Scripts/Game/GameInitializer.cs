using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public static class GameInitializer
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Initialize()
    {
        InitializeServices();
    }

    static void InitializeServices()
    {
        ServiceLocator.RegisterSO<FruitFactory>("Factories/FruitFactory");
        ServiceLocator.RegisterSO<ObstacleFactory>("Factories/ObstacleFactory");
    }
}