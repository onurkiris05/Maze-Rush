using Game.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Managers
{
    public class SceneController : MonoBehaviour, ISceneController
    {
        #region PUBLIC METHODS

        public void RestartScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        #endregion
    }
}