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
        public event Action OnLevelCompleted;
        public GameState State { get; private set; }


        #region UNITY EVENTS

        private void Awake()
        {
            if (!IsNewGame() && !IsLevelLoaded())
            {
                Debug.Log("Loading level");
                sceneController.LoadScene(PlayerPrefs.GetInt("CurrentLevel", 0));
            }

            State = GameState.Start;
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
                case GameState.Start:
                    break;
                case GameState.Running:
                    break;
                case GameState.End:
                    break;
            }

            OnAfterStateChanged?.Invoke(newState);
            Debug.Log($"New state: {newState}");
        }

        // Invoke from TAP TO START button
        public void InvokeOnStartGame()
        {
            ChangeState(GameState.Running);
            SetNewGame();
        }

        // Invoke from LEVEL COMPLETED button
        public void InvokeOnLevelCompleted() => OnLevelCompleted?.Invoke();

        public bool IsNewGame() => PlayerPrefs.GetInt("IsNewGame", 1) == 1;

        public void SetNewGame() => PlayerPrefs.SetInt("IsNewGame", 0);

        public int GetLevel() => PlayerPrefs.GetInt("CurrentLevel", 0) + 1;

        #endregion

        #region PRIVATE METHODS

        private bool IsLevelLoaded() => sceneController.CheckIsSceneLoaded();

        #endregion
    }

    public enum GameState
    {
        Start,
        Running,
        End
    }
}