using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using RunnerGame.Level;
using TMPro;
using UnityEngine;

namespace RunnerGame.UI
{
    public class LevelScoreBar : MonoBehaviour
    {
        [SerializeField] TMP_Text scoreText;
        [SerializeField] SerializedDictionary<Fruit.FruitType, TMP_Text> fruitTexts;

        public void SetLevelData(LevelData data)
        {
            scoreText.text = data.TotalScore.ToString();

            foreach (var (fruit, count) in data.FruitsCollected)
                fruitTexts[fruit].text = count.ToString();
        }
    }
}