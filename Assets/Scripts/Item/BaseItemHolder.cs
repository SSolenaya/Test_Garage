using System;
using UnityEngine;

namespace TestGarage
{
    public enum ItemState
    {
        none,
        idle,
        drag,
        picked
    }

    public class BaseItemHolder : MonoBehaviour
    {
        private const float TIME_MOVE_IDLE = 0.5f;
        private const float TIME_MOVE_PICKED = 0.3f;
        
        public ItemState CurrentItemState { get; private set; }
        
        private ItemView _itemView;
        private Camera _camera;
        private LayerMask _layerMask;
        private LevelController _levelController;
        private Action<BaseItemHolder> _onStateChange;
        
        private Rigidbody _rigidbody;
       
        private MoveItem _moveItem;

        private Vector3 _startAngle;
        private Vector3 _startPosition;
        private Vector3 _currentDragPosition;
        private Vector3 _pickedPosition = new Vector3(-0.47299999f, -0.419999987f, -1.20500004f);
        private Vector3 _finalPosition;

        private bool _isAboveCart;
        

        public void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Setup(ItemView itemView, Camera camera, LayerMask layerMask, Action<BaseItemHolder> onStateChange, LevelController levelController)
        {
            _itemView = itemView;
            _camera = camera;
            _layerMask = layerMask;
            _onStateChange = onStateChange;
            _levelController = levelController;


            transform.position = _itemView.transform.position;
            _itemView.transform.SetParent(transform);

            _startPosition = _itemView.transform.position;

            _startAngle = _itemView.transform.eulerAngles;
            transform.eulerAngles = _startAngle;
            _itemView.transform.localEulerAngles = Vector3.zero;

            _moveItem = new MoveItem(transform);

            _itemView.ActionOnPointerDown += OnPointerDown;
            _itemView.ActionOnDrag += OnDrag;
            _itemView.ActionOnPointerUp += OnPointerUp;

            SetState(ItemState.idle);
        }

        private void SetState(ItemState newItemState)
        {
            if (CurrentItemState == newItemState)
            {
                return;
            }

            _itemView.SetLayer(false);
            switch (newItemState)
            {
                case ItemState.none:
                    break;
                case ItemState.idle:
                    _rigidbody.isKinematic = true;
                    _moveItem.MoveTo(_startPosition, TIME_MOVE_IDLE);
                    _moveItem.RotateTo(_startAngle, TIME_MOVE_IDLE);
                    break;
                case ItemState.drag:
                    _rigidbody.isKinematic = true;
                    _itemView.SetLayer(true);
                    break;
                case ItemState.picked:

                    Vector3 pos = _finalPosition;
                    pos.y = _pickedPosition.y;
                    _moveItem.MoveTo(pos, TIME_MOVE_PICKED, () =>
                    {
                        _rigidbody.isKinematic = false;
                    });
                    
                    break;
            }

            CurrentItemState = newItemState;
            _onStateChange.Invoke(this);
        }


        private void OnPointerDown(Vector3 position)
        {
            if (_levelController.CurrentLevelState != LevelState.Game)
            {
                return;
            }
            SetState(ItemState.drag);
        }

        private void OnDrag(Vector3 position)
        {
            if (_levelController.CurrentLevelState != LevelState.Game)
            {
                return;
            }

            _currentDragPosition = GetWorldPosition(position); 
            _moveItem.MoveTo(_currentDragPosition);
            CheckRayCast(position);
        }

        private void OnPointerUp(Vector3 position)
        {
            if (_levelController.CurrentLevelState != LevelState.Game)
            {
                return;
            }
            if (_isAboveCart)
            {
                SetState(ItemState.picked);
                return;
            }

            SetState(ItemState.idle);
        }

        private Vector3 GetWorldPosition(Vector3 position)
        {
            Vector3 v3Pos = position;
            v3Pos.z = 3; //TODO move to SO
            return _camera.ScreenToWorldPoint(v3Pos);
        }

        private void CheckRayCast(Vector3 position)
        {
            Ray ray = Camera.main.ScreenPointToRay(position);
            _isAboveCart = false;

            if (Physics.Raycast(ray, out RaycastHit hit, 100, _layerMask))
            {
                CartPlace cartPlace = hit.transform.GetComponent<CartPlace>();
                if (cartPlace != null)
                {
                    _finalPosition = hit.point;
                       _isAboveCart = true;
                }
            }
        }

        private void OnDestroy()
        {
            if (_itemView == null)
            {
                return;
            }

            _itemView.ActionOnPointerDown -= OnPointerDown;
            _itemView.ActionOnDrag -= OnDrag;
            _itemView.ActionOnPointerUp -= OnPointerUp;
        }
    }
}


