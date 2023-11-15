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

                // Get collide position for VFX 
                if (other.TryGetComponent(out Collider collider))
                {
                    var rawHitPos = collider.ClosestPointOnBounds(transform.position);
                    var hitPos = new Vector3(rawHitPos.x, player.transform.position.y, rawHitPos.z);
                    VFXSpawner.Instance.PlayVFX("SawTrapHit", hitPos);
                }

                player.ProcessDefeated();
            }
        }

        #endregion


        #region PRIVATE METHODS

        protected override void Init()
        {
            transform.DORotate(new Vector3(0f, 180f, 0f), trapSpeed).SetSpeedBased()
                .SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
        }

        #endregion
    }
}
