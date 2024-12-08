using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RunnerGame.Level
{
    [CreateAssetMenu(fileName = "LevelSettings", menuName = "Runner/LevelSettings")]
    public class LevelSettingsSO : ScriptableObject
    {
        [field: SerializeField] 
        public LevelGenerationSettingsSO LevelGenerationSettings { get; private set; }


        [field: SerializeField, Min(1)] 
        public float PlayerStartSpeed { get; private set; }


    }
}