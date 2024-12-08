
using TMPro;
using UnityEngine;

public class LevelUI : MonoBehaviour
{
    [SerializeField] LevelScoreBar scoreBar;
    [SerializeField] GameOverPopup gameOverPopup;

    public void Init()
    {
        scoreBar.Init();
    }
}
