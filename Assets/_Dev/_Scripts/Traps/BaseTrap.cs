using UnityEngine;

namespace Game.Traps
{
    public abstract class BaseTrap : MonoBehaviour
    {
        protected abstract void OnTriggerEnter(Collider other);
    }
}