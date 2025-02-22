using UnityEngine;

namespace Game.Player
{
    public class AnimationHandler : MonoBehaviour
    {
        private PlayerController _player;
        private Animator _animator;
        private readonly string _blendParameter = "Blend";
        private readonly string _deadTrigger = "Dead";


        #region UNITY EVENTS

        private void Awake() => _animator = GetComponent<Animator>();

        #endregion


        #region PUBLIC METHODS

        public void Init(PlayerController player) => _player = player;

        public void UpdateBlendValue(float magnitude)
        {
            _animator.SetFloat(_blendParameter, magnitude);
        }

        public void PlayDeathAnimation()
        {
            _animator.SetTrigger(_deadTrigger);
        }

        #endregion
    }
}