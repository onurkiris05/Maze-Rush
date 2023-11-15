namespace Game.Interfaces
{
    public interface ISceneController
    {
        void LoadNextScene();
        void LoadScene(int sceneIndex);
        bool CheckIsSceneLoaded();
    }
}