using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace TestGarage
{
    public class CarDoor : MonoBehaviour
    {
        [SerializeField] private float _timeOpenDoor;
        [SerializeField] private Vector3 _finalAngle;


        public async UniTask OpenDoor()
        {
            transform.DOLocalRotate(_finalAngle, _timeOpenDoor).SetEase(Ease.Linear);

            await UniTask.Delay(TimeSpan.FromSeconds(_timeOpenDoor));
        }

        public async UniTask CloseDoor()
        {
            transform.DOLocalRotate(Vector3.zero, _timeOpenDoor).SetEase(Ease.Linear);

            await UniTask.Delay(TimeSpan.FromSeconds(_timeOpenDoor));
        }
    }
}