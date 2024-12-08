
using RunnerGame.Level;
using TMPro;
using UnityEngine;

namespace RunnerGame.UI
{
    public class LevelUI : MonoBehaviour
    {
        [SerializeField] LevelScoreBar scoreBar;
        [SerializeField] GameOverPopup gameOverPopup;


        public void UpdateLevelData(LevelData data)
        {
            scoreBar.SetLevelData(data);
        }

        public void ShowGameOverPopup(int finalScore)
        {
            gameOverPopup.SetFinalScore(finalScore);
            gameOverPopup.Show();
        }
    }
}