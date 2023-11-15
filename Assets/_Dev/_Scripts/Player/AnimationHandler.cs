using UnityEngine;

namespace Game.Player
{
    public class AnimationHandler : MonoBehaviour
    {
        private PlayerController _player;
        private Animator _animator;
        private readonly string _blendParameter = "Blend";


        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Init(PlayerController player) => _player = player;

        public void UpdateBlendValue(float magnitude)
        {
            _animator.SetFloat(_blendParameter, magnitude);
        }
    }
}