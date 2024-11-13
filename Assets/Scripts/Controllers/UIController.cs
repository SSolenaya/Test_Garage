using UnityEngine;

namespace TestGarage
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private Transform _endGame;

        public void SetActiveEndGame(bool isActive)
        {
            _endGame.gameObject.SetActive(isActive);
        }
    }
}