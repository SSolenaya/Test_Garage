using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace TestGarage
{
    public enum LevelState
    {
        Menu,
        Game
    }

    public class LevelController : MonoBehaviour
    {
        [Inject] private CameraController _cameraController;
        [Inject] private UIController _uiController;

        [SerializeField] private readonly float _startDelay = 1f;
        [SerializeField] private readonly float _endGameDelay = 4f;
        [SerializeField] private readonly float _timeMoveCamera = 2f;

        [SerializeField] private Transform _pointAbovePickup;
        public Transform PointAbovePickup => _pointAbovePickup;

        [SerializeField] private Transform _cameraInside;
        [SerializeField] private Transform _cameraOutside;
        [SerializeField] private GarageDoor _garageDoor;
        [SerializeField] private CarDoor _carDoor;
        [SerializeField] private Transform _carBackDoor;

        [SerializeField] private List<Shelf> _listShelf;
        public List<Shelf> ListShelf => _listShelf;

        public LevelState CurrentLevelState { get; private set; }


        public void Awake()
        {
            _listShelf = transform.GetComponentsInChildren<Shelf>().ToList();
        }

        public void Start()
        {
            StartLevel().Forget();
        }

        private async UniTask StartLevel()
        {
            SetState(LevelState.Menu);
            _uiController.SetActiveEndGame(false);

            await UniTask.Delay(TimeSpan.FromSeconds(_startDelay));
            await _garageDoor.OpenDoor();
            _carDoor.OpenDoor().Forget();
            await _cameraController.MoveAndRotateCamera(_cameraInside, _timeMoveCamera);

            SetState(LevelState.Game);
        }

        public async UniTask EndLevel()
        {
            SetState(LevelState.Menu);
            _uiController.SetActiveEndGame(true);
            _carDoor.CloseDoor().Forget();
            await UniTask.Delay(TimeSpan.FromSeconds(_endGameDelay));

            SceneManager.LoadScene(0);
        }

        private void SetState(LevelState newLevelState)
        {
            if (CurrentLevelState == newLevelState)
            {
                return;
            }

            CurrentLevelState = newLevelState;
        }
    }
}