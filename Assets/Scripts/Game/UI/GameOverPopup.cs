using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverPopup : Popup
{
    [SerializeField] TMP_Text gameOverScore;

    public event Action OnExitClicked;

    public void SetFinalScore(int finalScore)
    {
        gameOverScore.text = finalScore.ToString();
    }

    public void ExitGame()
    {
        OnExitClicked?.Invoke();
    }
}
