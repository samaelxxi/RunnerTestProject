using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using RunnerGame.Level;
using TMPro;
using UnityEngine;

public class LevelScoreBar : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] SerializedDictionary<Fruit.FruitType, TMP_Text> fruitTexts;

    public void Init()
    {
        scoreText.text = "0";

        foreach (var text in fruitTexts.Values)
            text.text = "0";
    }
}
