using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RunnerGame.UI
{
    public class GameOverPopup : Popup
    {
        [SerializeField] TMP_Text gameOverScore;


        public void SetFinalScore(int finalScore)
        {
            gameOverScore.text = finalScore.ToString();
        }

        public void ExitGame()
        {
            ServiceLocator.Get<SceneTransitionManager>().StartTransition("MainMenu");
        }
    }
}