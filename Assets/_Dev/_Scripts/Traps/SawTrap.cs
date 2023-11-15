using DG.Tweening;
using Game.Player;
using UnityEngine;

namespace Game.Traps
{
    public class SawTrap : BaseTrap
    {
        [Header("Settings")]
        [SerializeField] private float trapSpeed;

        [Space] [Header("Components")]
        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform endPoint;
        [SerializeField] private GameObject sawObject;

        private bool _isTriggered;


        #region UNITY EVENTS

        private void Start() => Init();

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

        private void Init()
        {
            sawObject.transform.position = startPoint.position;
            sawObject.transform.DOMove(endPoint.position, trapSpeed).SetEase(Ease.Linear).SetSpeedBased()
                .SetLoops(-1, LoopType.Yoyo);
            sawObject.transform.DOLocalRotate(new Vector3(0f, 0f, 180f), 0.5f)
                .SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
        }

        private void InteractEffect()
        {
            _isTriggered = true;

            transform.DOComplete();
            transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.5f).From()
                .OnComplete(() => _isTriggered = false);
        }

        #endregion
    }
}