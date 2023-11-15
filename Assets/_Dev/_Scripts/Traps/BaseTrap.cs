using DG.Tweening;
using UnityEngine;

namespace Game.Traps
{
    public abstract class BaseTrap : MonoBehaviour
    {
        protected bool _isTriggered;


        #region VIRTUAL METHODS

        protected virtual void Start() => Init();

        protected virtual void InteractEffect()
        {
            _isTriggered = true;

            transform.DOComplete();
            transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.5f).From();
        }

        #endregion


        #region ABSTRACT METHODS

        protected abstract void OnTriggerEnter(Collider other);
        protected abstract void Init();

        #endregion
    }
}