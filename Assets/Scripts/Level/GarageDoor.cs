using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace TestGarage
{
    public class GarageDoor : MonoBehaviour
    {
        [SerializeField] private float _timeOpenDoor;
        [SerializeField] private Vector3 _finalPosition;
        [SerializeField] private Vector3 _finalScale;


        public async UniTask OpenDoor()
        {
            transform.DOLocalMove(_finalPosition, _timeOpenDoor).SetEase(Ease.Linear);
            transform.DOScale(_finalScale, _timeOpenDoor).SetEase(Ease.Linear);

            await UniTask.Delay(TimeSpan.FromSeconds(_timeOpenDoor));
        }
    }
}
