using System;
using DG.Tweening;
using UnityEngine;

namespace TestGarage
{
    public class MoveItem
    {
        private Tween _moveTween;
        private Tween _rotateTween;

        private readonly Transform _transform;

        public MoveItem(Transform transform)
        {
            _transform = transform;
        }

        public void MoveTo(Vector3 finalPosition, float time = 0.05f, Action onCompleted = null)
        {
            _moveTween?.Kill();
            _moveTween = _transform.DOMove(finalPosition, time).SetEase(Ease.Linear).OnComplete(() => { onCompleted?.Invoke(); });
        }

        public void RotateTo(Vector3 finalAngle, float time = 0.05f, Action onCompleted = null)
        {
            _rotateTween?.Kill();
            _rotateTween = _transform.DORotate(finalAngle, time).SetEase(Ease.Linear).OnComplete(() => { onCompleted?.Invoke(); });
        }
    }
}