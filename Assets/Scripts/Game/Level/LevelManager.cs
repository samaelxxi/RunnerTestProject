using System;
using System.Collections;
using System.Collections.Generic;
using RunnerGame.SaveSystem;
using RunnerGame.UI;
using UnityEngine;


namespace RunnerGame.Level
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] Player player;
        [SerializeField] Level level;
        [SerializeField] LevelUI ui;

        void Start()
        {
            level.OnLevelDataChange += ui.UpdateLevelData;
            level.SetPlayer(player);

            player.OnDeath += EndGame;
        }

        void EndGame()
        {
            AddNewRecord();
            ui.ShowGameOverPopup(level.Data.TotalScore);
        }

        void AddNewRecord()
        {
            PlayerRecordData record = new()
            {
                TotalScore = level.Data.TotalScore,
                GameTime = Time.time - level.StartTime,
                GameDate = DateTime.Now
            };

            var records = ServiceLocator.Get<SaveService>()
                .GetSaveable<PlayerRecordsSaveable>() as PlayerRecordsSaveable;
            records.AddNewRecord(record);
        }
    }
}
