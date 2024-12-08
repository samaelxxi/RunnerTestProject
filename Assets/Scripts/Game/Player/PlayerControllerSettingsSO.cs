using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RunnerGame.Game
{
    [CreateAssetMenu(fileName = "PlayerControllerSettings", menuName = "Runner/PlayerControllerSettings")]
    public class PlayerControllerSettingsSO : ScriptableObject
    {
        [field: SerializeField, Min(0)] public float StartForwardSpeed { get; private set; }
        [field: SerializeField, Min(0)] public float ForwardAcceleration { get; private set; }
        [field: SerializeField, Min(0)] public float ForwardMaxSpeed { get; private set; }
        [field: SerializeField, Min(0)] public float HorizontalSpeed { get; private set; }
    }
}