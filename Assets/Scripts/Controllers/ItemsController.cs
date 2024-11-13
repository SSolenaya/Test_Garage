using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace TestGarage
{
    public class ItemsController : MonoBehaviour
    {
        [Inject] private CameraController _cameraController;
        [Inject] private LevelController _levelController;

        [SerializeField] private BaseItemHolder _baseItemHolderPrefab;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private List<BaseItemHolder> _listBaseItemHolder;


        private void Start()
        {
            SetupItems();
        }

        private void SetupItems()
        {
            foreach (Shelf shelf in _levelController.ListShelf)
            {
                foreach (ItemView itemView in shelf.ListItemsView)
                {
                    BaseItemHolder baseItemHolder = Instantiate(_baseItemHolderPrefab);
                    _listBaseItemHolder.Add(baseItemHolder);

                    baseItemHolder.Setup(itemView, _cameraController.MainCamera, _layerMask, ItemChangeState, _levelController);
                }
            }
        }

        private void ItemChangeState(BaseItemHolder itemHolder)
        {
            int countPicked = 0;

            foreach (BaseItemHolder baseItemHolder in _listBaseItemHolder)
            {
                if (baseItemHolder.CurrentItemState == ItemState.picked)
                {
                    countPicked++;
                }
            }

            if (countPicked == _listBaseItemHolder.Count)
            {
                _levelController.EndLevel().Forget();
            }
           

        }
    }
}

