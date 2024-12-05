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
            public GameObject FruitPrefab;
            public float FruitScore;
            public float FruitProbability;
        }

        [field: SerializeField, Min(1)] public float LevelLength { get; private set; }
        [field: SerializeField] public float ReferenceObjectPositionOffset { get; private set; }

        [field: Space(5)]
        [field: SerializeField] public GameObject RoadSegmentPrefab { get; private set; }
        [field: SerializeField] public float RoadSegmentLength { get; private set; }
        [field: SerializeField] public List<FruitData> Fruits { get; private set; }
        [field: SerializeField] public List<GameObject> ObstaclePrefabs { get; private set; }
    }
}