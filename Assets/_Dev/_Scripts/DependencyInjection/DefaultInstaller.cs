using Game.Interfaces;
using Game.Managers;
using Zenject;

public class DefaultInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        // MANAGER COMPONENTS
        Container.Bind<IGameManager>().To<GameManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ISceneController>().To<SceneController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ICameraManager>().To<CameraManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IUIManager>().To<UIManager>().FromComponentInHierarchy().AsSingle();
    }
}