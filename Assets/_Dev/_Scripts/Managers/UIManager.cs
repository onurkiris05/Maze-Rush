using Game.Interfaces;
using UnityEngine;
using Zenject;

namespace Game.Managers
{
    public class UIManager : MonoBehaviour, IUIManager
    {
        [Header("Components")]
        [SerializeField] private GameObject winCanvas;
        [SerializeField] private GameObject failedCanvas;

        [Inject] private IGameManager gameManager;


        #region UNITY EVENTS

        private void OnEnable()
        {
            gameManager.OnAfterStateChanged += CheckState;
        }

        private void OnDisable()
        {
            gameManager.OnAfterStateChanged -= CheckState;
        }

        #endregion


        #region PRIVATE METHODS

        private void CheckState(GameState state)
        {
            winCanvas.SetActive(state == GameState.Win);
            failedCanvas.SetActive(state == GameState.Fail);
        }

        #endregion
    }
}