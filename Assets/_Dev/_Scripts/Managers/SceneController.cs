using Game.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Managers
{
    public class SceneController : MonoBehaviour, ISceneController
    {
        #region PUBLIC METHODS

        public void LoadNextScene()
        {
            var currentIndex = PlayerPrefs.GetInt("CurrentLevel", 0);
            LoadScene(currentIndex + 1);
        }

        public void LoadScene(int sceneIndex)
        {
            if (sceneIndex > SceneManager.sceneCount)
            {
                Debug.Log("Scene index out of range. Loading very last scene!");
                sceneIndex = SceneManager.sceneCount;
            }

            PlayerPrefs.SetInt("CurrentLevel", sceneIndex);
            SceneManager.LoadScene(sceneIndex);
        }

        public bool CheckIsSceneLoaded()
        {
            var currentIndex = PlayerPrefs.GetInt("CurrentLevel", 0);
            return SceneManager.GetActiveScene().buildIndex == currentIndex;
        }

        #endregion
    }
}