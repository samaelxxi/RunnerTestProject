using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RunnerGame.Level
{
    public class Level : MonoBehaviour
    {
        [SerializeField] LevelSettingsSO levelSettings;
        [SerializeField] LevelGenerator levelGenerator;


        void Awake()
        {
            levelGenerator.SetGenerationSettings(levelSettings.LevelGenerationSettings);
        }

        void Start()
        {
            
        }

        void Update()
        {
            
        }

        public void SetPlayer(Player player)
        {
            levelGenerator.SetReferenceObject(player.transform);
            levelGenerator.InitGenerate();
        }
    }
}