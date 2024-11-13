using UnityEngine;
using Zenject;

namespace TestGarage
{
    [CreateAssetMenu(fileName = "PrefabInstaller", menuName = "ScriptableObjects/PrefabInstaller", order = 1)]
    public class PrefabInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private LevelController _levelController;
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private ItemsController _itemsController;
        [SerializeField] private UIController _uiController;

        public override void InstallBindings()
        {
            Container.Bind<CameraController>().FromComponentInNewPrefab(_cameraController).AsSingle().NonLazy();
            Container.Bind<LevelController>().FromComponentInNewPrefab(_levelController).AsSingle().NonLazy();
            Container.Bind<ItemsController>().FromComponentInNewPrefab(_itemsController).AsSingle().NonLazy();
            Container.Bind<UIController>().FromComponentInNewPrefab(_uiController).AsSingle().NonLazy();
        }
    }
}