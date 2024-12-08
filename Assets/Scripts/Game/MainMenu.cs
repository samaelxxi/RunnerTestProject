using System.Linq;
using System.Collections;
using UnityEngine;
using RunnerGame.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] PlayerRecordsWindow records;

    void Start()
    {
        records.SetupPlayerRecords();

        ServiceLocator.Get<FruitFactory>().ReleaseAll();
        ServiceLocator.Get<ObstacleFactory>().ReleaseAll();
    }

    public void StartGame()
    {
        ServiceLocator.Get<SceneTransitionManager>().StartTransition("GameScene");
    }
}
