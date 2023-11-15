using DG.Tweening;
using Game.Player;
using UnityEngine;

namespace Game.Traps
{
    public class SawTrap : BaseTrap
    {
        [Header("Settings")]
        [SerializeField] private int pushBackCount;
        [SerializeField] private float pushBackDistance;
        [SerializeField] private float pushBackDuration;

        [Header("Components")]
        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform endPoint;
        [SerializeField] private GameObject sawObject;

        private bool _isTriggered;

        #region UNITY EVENTS

        private void Start() => Init();
        
        protected override void OnTriggerEnter(Collider other)
        {
            // if (other.TryGetComponent(out PlayerController player))
            // {
            //     if (_isTriggered) return;
            //
            //     InteractEffect();
            //     player.PushBack(pushBackDistance, pushBackDuration);
            //
            //     // Get collide position for VFX 
            //     if (other.TryGetComponent(out Collider collider))
            //     {
            //         var rawHitPos = collider.ClosestPointOnBounds(transform.position);
            //         var hitPos = new Vector3(rawHitPos.x, player.transform.position.y, rawHitPos.z);
            //         // VFXSpawner.Instance.PlayVFX("TrapHit", hitPos);
            //     }
            //
            //     pushBackCount--;
            //     Debug.Log($"Pushback count left: {pushBackCount}");
            //     if (pushBackCount <= 0)
            //         Kill();
            // }
        }

        #endregion

        #region PRIVATE METHODS

        private void Init()
        {
            sawObject.transform.position = startPoint.position;
            sawObject.transform.DOMove(endPoint.position, 1f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
            sawObject.transform.DOLocalRotate(new Vector3(0f, 0f, 180f), 0.5f)
                .SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
        }

        private void InteractEffect()
        {
            _isTriggered = true;

            transform.DOComplete();
            transform.DOScale(new Vector3(0.7f, 0.7f, 0.7f), 0.2f).From()
                .OnComplete(() => _isTriggered = false);
        }

        private void Kill()
        {
            transform.DOComplete();
            transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack)
                .OnComplete(() => Destroy(gameObject));
        }

        #endregion
    }
}