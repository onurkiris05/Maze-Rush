using DG.Tweening;
using Game.Player;
using UnityEngine;

namespace Game.Traps
{
    public class SpinnerTrap : BaseTrap
    {
        [Header("Settings")]
        [SerializeField] private float trapSpeed;


        #region UNITY EVENTS

        protected override void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerController player))
            {
                if (_isTriggered) return;

                InteractEffect();

                player.ProcessDefeated();
            }
        }

        #endregion


        #region PROTECTED METHODS

        protected override void Init()
        {
            transform.DORotate(new Vector3(0f, 180f, 0f), trapSpeed).SetSpeedBased()
                .SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
        }

        #endregion
    }
}
