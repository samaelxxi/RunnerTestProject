using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RunnerGame.Game;


namespace RunnerGame.Level
{
    public struct LevelData
    {
        public Dictionary<Fruit.FruitType, int> FruitsCollected;
        public int TotalScore;
    }

    public class Level : MonoBehaviour
    {
        [SerializeField] LevelSettingsSO levelSettings;
        [SerializeField] LevelGenerator levelGenerator;


        public event Action<LevelData> OnLevelDataChange;

        public float StartTime;
        public LevelData Data => _data;

        LevelData _data;

        void Awake()
        {
            levelGenerator.SetGenerationSettings(levelSettings.LevelGenerationSettings);
        }

        public void SetPlayer(Player player)
        {
            player.SetHorizontalLimit(levelSettings.LevelGenerationSettings.LevelWidth / 2.0f);
            levelGenerator.SetReferenceObject(player.transform);
            levelGenerator.InitGenerate();

            _data = new()
            {
                FruitsCollected = new(),
                TotalScore = 0
            };
            var fruitTypes = Enum.GetValues(typeof(Fruit.FruitType)).Cast<Fruit.FruitType>();
            foreach (var fruitType in fruitTypes)
                _data.FruitsCollected[fruitType] = 0;
            OnLevelDataChange?.Invoke(_data);

            player.OnFruitCollected += FruitCollected;

            StartTime = Time.time;
        }

        void FruitCollected(Fruit fruit)
        {
            _data.FruitsCollected[fruit.Type]++;
            _data.TotalScore += levelSettings.FruitScores[fruit.Type];
            OnLevelDataChange?.Invoke(_data);
        }

        void OnDrawGizmosSelected()
        {
            if (levelSettings != null)
            {
                Gizmos.color = Color.green;
                float width = levelSettings.LevelGenerationSettings.LevelWidth / 2.0f;
                Gizmos.DrawLine(transform.position.SetX(transform.position.x - width),
                                transform.position.SetX(transform.position.x + width));
            }
        }
    }
}