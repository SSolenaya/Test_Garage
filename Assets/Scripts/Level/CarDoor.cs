using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace TestGarage
{
    public class CarDoor : MonoBehaviour
    {
        [SerializeField] public float timeOpenDoor;
        [SerializeField] public Vector3 finalAngle;

        public async UniTask OpenDoor()
        {
            transform.DOLocalRotate(finalAngle, timeOpenDoor).SetEase(Ease.Linear);

            await UniTask.Delay(TimeSpan.FromSeconds(timeOpenDoor));
        }

        public async UniTask CloseDoor()
        {
            transform.DOLocalRotate(Vector3.zero, timeOpenDoor).SetEase(Ease.Linear);

            await UniTask.Delay(TimeSpan.FromSeconds(timeOpenDoor));
        }
    }
}