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
            sawObject.transform.position = startPoint.position;
            sawObject.transform.DOMove(endPoint.position, trapSpeed).SetEase(Ease.Linear).SetSpeedBased()
                .SetLoops(-1, LoopType.Yoyo);
            sawObject.transform.DOLocalRotate(new Vector3(0f, 0f, 180f), 0.5f)
                .SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
        }

        #endregion
    }
}