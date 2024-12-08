using System.Linq;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using RunnerGame.SaveSystem;

public class MainMenu : MonoBehaviour
{
    [SerializeField] PlayerRecordsWindow records;

    void Start()
    {
        records.SetupPlayerRecords();
    }

    public void StartGame()
    {
        ServiceLocator.Get<SceneTransitionManager>().StartTransition("GameScene");
    }
}
