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

        protected override void Init()
        {
            hammerModel.DOLocalRotate(new Vector3(90f, 0f, 0f), trapSpeed).SetSpeedBased()
                .SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InCubic).SetRelative();
        }
    }
}