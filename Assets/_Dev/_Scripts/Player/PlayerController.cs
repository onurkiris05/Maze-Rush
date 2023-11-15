using UnityEngine;

namespace Game.Player
{
    public class PlayerController : MonoBehaviour
    {
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

        #endregion
    }
}