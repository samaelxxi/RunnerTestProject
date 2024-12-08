using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RunnerGame.Level
{
    [CreateAssetMenu(fileName = "LevelGenerationSettings", menuName = "Runner/LevelGenerationSettings")]
    public class LevelGenerationSettingsSO : ScriptableObject
    {
        [Serializable]
        public struct FruitData
        {
            public Fruit.FruitType FruitType;
            public float FruitScore;
            public float FruitProbability;
        }

        [field: SerializeField, Min(1)] public float LevelLength { get; private set; }
        [field: SerializeField, Min(1)] public float LevelWidth { get; private set; }
        [field: SerializeField] public float ReferenceObjectRoadOffset { get; private set; }

        [field: SerializeField, Min(0)] public int StartEmptySegments { get; private set; }

        [field: Header("Road")]
        [field: SerializeField, Min(0)] public int NoItemsSegments { get; private set; }
        [field: SerializeField] public GameObject RoadSegmentPrefab { get; private set; }
        [field: SerializeField, Min(1)] public float RoadSegmentLength { get; private set; }
        [field: SerializeField, Min(0)] public int MaxItemsPerSegment { get; private set; }

        [field: Header("Fruits")]
        [field: SerializeField] public BinomialDistribution FruitDistribution { get; private set; }
        [field: SerializeField] public List<FruitData> Fruits { get; private set; }

        [field: Header("Obstacles")]
        [field: SerializeField] public BinomialDistribution StartObstacleDistribution { get; private set; }
        [field: SerializeField, Range(0, 0.1f)] public float ObstacleProbabilityAcceleration { get; private set; }


        [field: SerializeField] public List<GameObject> EnvironmentObjects { get; private set; }
    }
}
