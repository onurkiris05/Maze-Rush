using Game.Interfaces;
using Game.Managers;
using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class PlayerController : MonoBehaviour
    {
        [Inject] private IGameManager gameManager;
        
        private MovementHandler _movementHandler;
        private AnimationHandler _animationHandler;
        
        
        #region UNITY EVENTS

        private void Awake()
        {
            _movementHandler = GetComponent<MovementHandler>();
            _animationHandler = GetComponent<AnimationHandler>();
            
            _movementHandler.Init(this);
            _animationHandler.Init(this);
        }

        #endregion
        

        #region PUBLIC METHODS

        public void ProcessMovementMagnitude(float movementMagnitude)
        {
            _animationHandler.UpdateBlendValue(movementMagnitude);
        }
        
        public void ProcessVictorious()
        {
            gameManager.ChangeState(GameState.Win);
            _animationHandler.UpdateBlendValue(0f);
        }

        public void ProcessDefeated()
        {
            gameManager.ChangeState(GameState.Fail);
            _animationHandler.PlayDeathAnimation();
        }

        #endregion
    }
}