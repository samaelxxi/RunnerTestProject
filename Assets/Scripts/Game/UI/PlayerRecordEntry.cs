using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerRecordEntry : MonoBehaviour
{
    [SerializeField] TMP_Text dateText;
    [SerializeField] TMP_Text timeText;
    [SerializeField] TMP_Text scoreText;

    public void SetupEntry(PlayerRecordData record)
    {
        dateText.text = record.GameDate.ToString();
        timeText.text = TimeSpan.FromSeconds(record.GameTime).ToString(@"mm\:ss");
        scoreText.text = record.TotalScore.ToString();
    }
}
