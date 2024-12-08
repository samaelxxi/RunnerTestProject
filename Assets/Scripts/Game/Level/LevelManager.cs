using System.Collections;
using System.Collections.Generic;
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
            level.SetPlayer(player);
            ui.Init();
        }
    }
}
