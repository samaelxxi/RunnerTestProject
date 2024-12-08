using System;
using System.Collections;
using System.Collections.Generic;
using DesignPatterns;
using DG.Tweening;
using UnityEngine;


namespace RunnerGame.Level
{
    public class Fruit : MonoBehaviour
    {
        public enum FruitType { Red, Orange, Yellow }

        [SerializeField] int score;
        [SerializeField] FruitType type;

        public event Action OnFruitCollected;

        private bool _isCollected;

        void Start()
        {
            transform.DOLocalMoveY(transform.localPosition.y + 1, 2f)
                .SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
            transform.DORotate(new Vector3(0, 360, 0), 2f, RotateMode.FastBeyond360)
                .SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
        }

        public void Collect()
        {
            if (_isCollected) return;

            transform.DOScale(Vector3.zero, 1.5f).SetEase(Ease.InOutSine);
            _isCollected = true;
        }

        public void Spawn()
        {
            transform.localScale = Vector3.one;
            _isCollected = false;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Player>(out var player))
            {
                OnFruitCollected?.Invoke();
                Collect();
            }
        }
    }
}