using System;
using Game.Interfaces;
using UnityEngine;
using Zenject;

namespace Game.Managers
{
    public class GameManager : MonoBehaviour, IGameManager
    {
        [Inject] private ISceneController sceneController;

        public event Action<GameState> OnBeforeStateChanged;
        public event Action<GameState> OnAfterStateChanged;
        public GameState State { get; private set; }


        #region UNITY EVENTS

        private void Awake()
        {
            State = GameState.Playing;
        }

        #endregion


        #region PUBLIC METHODS

        public void ChangeState(GameState newState)
        {
            if (newState == State) return;

            OnBeforeStateChanged?.Invoke(newState);

            State = newState;
            switch (newState)
            {
                case GameState.Playing:
                    break;
                case GameState.Victorious:
                    break;
                case GameState.Defeated:
                    break;
            }

            OnAfterStateChanged?.Invoke(newState);
            Debug.Log($"New GameState: {newState}");
        }

        #endregion
    }

    public enum GameState
    {
        Playing,
        Victorious,
        Defeated
    }
}