using System;
using System.Collections;
using Game.Interfaces;
using UnityEngine;
using Zenject;

namespace Game.Managers
{
    public class GameManager : MonoBehaviour, IGameManager
    {
        [Inject] private ISceneController sceneController;
        [Inject] private ICameraManager cameraManager;

        public event Action<GameState> OnBeforeStateChanged;
        public event Action<GameState> OnAfterStateChanged;
        public GameState State { get; private set; } = GameState.Playing;


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
                case GameState.Win:
                    cameraManager.SetCamera(CameraType.CloseLookUp);
                    StartCoroutine(ProcessGameRestart());
                    break;
                case GameState.Fail:
                    cameraManager.SetCamera(CameraType.CloseLookUp);
                    StartCoroutine(ProcessGameRestart());
                    break;
            }

            OnAfterStateChanged?.Invoke(newState);
            Debug.Log($"New GameState: {newState}");
        }

        #endregion


        #region PRIVATE METHODS

        private IEnumerator ProcessGameRestart()
        {
            yield return Helpers.BetterWaitForSeconds(4f);
            sceneController.RestartScene();
        }

        #endregion
    }

    public enum GameState
    {
        Playing,
        Win,
        Fail
    }
}