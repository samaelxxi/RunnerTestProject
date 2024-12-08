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

    public FruitPoolable GetFruit(FruitType type)
    {
        var fruit = _pools[type].Get();
        fruit.Fruit.Spawn();
        return fruit;
    }

    protected override FruitPoolable CreateObject(FruitType key)
    {
        var obj = base.CreateObject(key);
        obj.Fruit.OnFruitDissapeared += () => obj.Release();

        return obj;
    }

    public void Initialize()
    {
        base.Initialize(fruits, "Fruits");
    }
}
