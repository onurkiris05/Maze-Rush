using Game.Interfaces;
using Game.Managers;
using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class PlayerController : MonoBehaviour
    {
        [Inject] private IGameManager gameManager;
        
        public bool IsDead { get; private set; }
        
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
            gameManager.ChangeState(GameState.Victorious);
        }

        public void ProcessDefeated()
        {
            IsDead = true;
            gameManager.ChangeState(GameState.Defeated);
        }

        #endregion
    }
}