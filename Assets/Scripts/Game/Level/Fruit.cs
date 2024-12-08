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

        [SerializeField] FruitType type;

        public event Action OnFruitDissapeared;

        public FruitType Type => type;
        public bool IsCollected { get; private set; }

        void Start()
        {
            transform.DOLocalMoveY(transform.localPosition.y + 1, 2f)
                .SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
            transform.DORotate(new Vector3(0, 360, 0), 2f, RotateMode.FastBeyond360)
                .SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
        }

        public void Collect()
        {
            if (IsCollected) return;

            transform.DOScale(Vector3.zero, 0.25f).SetEase(Ease.InOutSine).OnComplete(() => OnFruitDissapeared?.Invoke());
            IsCollected = true;
        }

        public void Spawn()
        {
            transform.localScale = Vector3.one;
            IsCollected = false;
        }
    }
}