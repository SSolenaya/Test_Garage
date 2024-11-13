using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TestGarage
{
    public class ItemView : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        public event Action<Vector3> ActionOnPointerDown;
        public event Action<Vector3> ActionOnDrag;
        public event Action<Vector3> ActionOnPointerUp;

        private Camera _camera;

        public void Setup(Camera camera)
        {
            _camera = camera;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            ActionOnPointerDown?.Invoke(eventData.position);
        }

        public void OnDrag(PointerEventData eventData)
        {
            ActionOnDrag?.Invoke(eventData.position);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            ActionOnPointerUp?.Invoke(eventData.position);
        }

        public void SetLayer(bool isItemMoving)
        {
            int layerNumber = isItemMoving ? 6 : 0;
            gameObject.layer = layerNumber;
        }

    }
}