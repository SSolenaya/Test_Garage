using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace TestGarage
{
    public class GarageDoor : MonoBehaviour
    {
        [SerializeField] public float timeOpenDoor;
        [SerializeField] public Vector3 finalPosition;
        [SerializeField] public Vector3 finalScale;

        public async UniTask OpenDoor()
        {
            transform.DOLocalMove(finalPosition, timeOpenDoor).SetEase(Ease.Linear);
            transform.DOScale(finalScale, timeOpenDoor).SetEase(Ease.Linear);

            await UniTask.Delay(TimeSpan.FromSeconds(timeOpenDoor));
        }
    }
}