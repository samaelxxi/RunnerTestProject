using System.Collections;
using System.Collections.Generic;
using DesignPatterns;
using RunnerGame.Level;
using UnityEngine;
using static RunnerGame.Level.Fruit;
using AYellowpaper.SerializedCollections;

[CreateAssetMenu(fileName = "FruitFactory", menuName = "Factories/FruitFactory")]
public class FruitFactory : Factory<FruitType, FruitPoolable>, Services.IInitializable, Services.IRegistrable
{
    [SerializeField] SerializedDictionary<FruitType, FruitPoolable> fruits;

    public FruitPoolable GetFruit(Fruit.FruitType type)
    {
        var fruit = _pools[type].Get();
        return fruit;
    }

    public void Initialize()
    {
        base.Initialize(fruits, "Fruits");
    }
}
