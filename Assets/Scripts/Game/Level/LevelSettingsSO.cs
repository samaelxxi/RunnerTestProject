using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;


namespace RunnerGame.Level
{
    [CreateAssetMenu(fileName = "LevelSettings", menuName = "Runner/LevelSettings")]
    public class LevelSettingsSO : ScriptableObject
    {
        [field: SerializeField] 
        public LevelGenerationSettingsSO LevelGenerationSettings { get; private set; }


        [field: SerializeField]
        public SerializedDictionary<Fruit.FruitType, int> FruitScores { get; private set; }
    }
}