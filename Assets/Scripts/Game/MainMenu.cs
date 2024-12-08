using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Transform recordsParent;
    [SerializeField] PlayerRecordEntry recordPrefab;

    void Start()
    {
        StartCoroutine(SetupPlayerRecords());
    }

    IEnumerator SetupPlayerRecords()
    {
        var saveService = ServiceLocator.Get<SaveService>();

        yield return new WaitUntil(() => saveService.IsLoaded);

        var recordsSaveable = saveService.GetSaveable<PlayerRecordsSaveable>() as PlayerRecordsSaveable;
        var records = recordsSaveable.Records();
        Debug.Log($"{records.Count()}");
        var sortedRecords = records.OrderBy(record => record.TotalScore);

        foreach (var record in sortedRecords)
        {
            Debug.Log($"{record.TotalScore}");
            var entry = Instantiate(recordPrefab, recordsParent);
            entry.SetupEntry(record);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadSceneAsync("GameScene");
    }
}
