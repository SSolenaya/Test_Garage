using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TestGarage
{
    public class ItemView : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        private const int LAYER_ITEM_MOVE = 6;

        public event Action<Vector3> ActionOnPointerDown;
        public event Action<Vector3> ActionOnDrag;
        public event Action<Vector3> ActionOnPointerUp;
        
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
            int layerNumber = isItemMoving ? LAYER_ITEM_MOVE : 0;
            gameObject.layer = layerNumber;
        }

    }
}