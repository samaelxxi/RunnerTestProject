using System;
using System.Collections;
using System.Collections.Generic;
using RunnerGame.Level;
using UnityEngine;


namespace RunnerGame.Game
{
    [SelectionBase]
    public class Player : MonoBehaviour
    {
        [SerializeField] PlayerController controller;
        [SerializeField] Animator animator;

        public event Action<Fruit> OnFruitCollected;
        public event Action OnDeath;

        static readonly int SpeedHash = Animator.StringToHash("Speed_f");

        public void SetHorizontalLimit(float limit)
        {
            controller.SetHorizontalLimit(limit);
        }

        void Start()
        {
            controller.StartRunning();
        }

        void Update()
        {
            animator.SetFloat(SpeedHash, controller.Speed);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Fruit>(out var fruit) && !fruit.IsCollected)
            {
                fruit.Collect();
                OnFruitCollected?.Invoke(fruit);
            }
            else
            {
                Die();
            }
        }

        public void Die()
        {
            controller.StopRunning();
            OnDeath?.Invoke();
        }
    }
}