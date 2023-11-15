using DG.Tweening;
using Game.Player;
using UnityEngine;

namespace Game.Traps
{
    public class HammerTrap : BaseTrap
    {
        [Header("Settings")]
        [SerializeField] private float trapSpeed;

        [Space] [Header("Components")]
        [SerializeField] private Transform hammerModel;
        

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
            hammerModel.DOLocalRotate(new Vector3(90f, 0f, 0f), trapSpeed).SetSpeedBased()
                .SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InCubic).SetRelative();
        }

        #endregion
    }
}