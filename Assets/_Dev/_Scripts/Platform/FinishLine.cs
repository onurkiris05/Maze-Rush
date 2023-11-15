using Game.Player;
using UnityEngine;

namespace Game.Platform
{
    public class FinishLine : MonoBehaviour
    {
        #region UNITY EVENTS

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerController player))
            {
                player.ProcessVictorious();
            }
        }

        #endregion
    }
}