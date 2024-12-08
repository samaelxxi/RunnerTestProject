using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RunnerGame.SaveSystem;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRecordsWindow : MonoBehaviour
{
    [SerializeField] Transform recordsParent;
    [SerializeField] PlayerRecordEntry recordPrefab;
    [SerializeField] ScrollRect scrollRect;

    public void SetupPlayerRecords()
    {
        StartCoroutine(SetupPlayerRecordsCoroutine());
    }

    IEnumerator SetupPlayerRecordsCoroutine()
    {
        var saveService = ServiceLocator.Get<SaveService>();

        yield return new WaitUntil(() => saveService.IsLoaded);

        var recordsSaveable = saveService.GetSaveable<PlayerRecordsSaveable>() as PlayerRecordsSaveable;
        var records = recordsSaveable.Records();
        var sortedRecords = records.OrderBy(record => record.TotalScore).Reverse();

        foreach (var record in sortedRecords)
        {
            var entry = Instantiate(recordPrefab, recordsParent);
            entry.SetupEntry(record);
        }
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 1;
    }
}
