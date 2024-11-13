using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace TestGarage
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Camera _mainCamera;
        public Camera MainCamera => _mainCamera;

        public async UniTask MoveAndRotateCamera(Transform finalTransform, float time)
        {
            _mainCamera.transform.DOMove(finalTransform.position, time);
            _mainCamera.transform.DORotate(finalTransform.eulerAngles, time);
            await UniTask.Delay(TimeSpan.FromSeconds(time));
        }

    }
}