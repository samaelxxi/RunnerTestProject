using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DesignPatterns;
using UnityEngine;
using AYellowpaper.SerializedCollections;


[CreateAssetMenu(fileName = "ObstacleFactory", menuName = "Factories/ObstacleFactory")]
public class ObstacleFactory : Factory<int, TransformPoolable>, Services.IInitializable, Services.IRegistrable
{
    [SerializeField] SerializedDictionary<int, TransformPoolable> obstacles;

    public TransformPoolable GetRandomObstacle()
    {
        var randomPool = _pools.Keys.ElementAt(Random.Range(0, _pools.Count));
        var obj = Get(randomPool);

        return obj;
    }

    public void Initialize()
    {
        base.Initialize(obstacles, "Obstacles");
    }
}
