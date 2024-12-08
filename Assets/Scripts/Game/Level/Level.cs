using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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


        void Awake()
        {
            levelGenerator.SetGenerationSettings(levelSettings.LevelGenerationSettings);
        }

        public void SetPlayer(Player player)
        {
            player.SetHorizontalLimit(levelSettings.LevelGenerationSettings.LevelWidth / 2.0f);
            levelGenerator.SetReferenceObject(player.transform);
            levelGenerator.InitGenerate();
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