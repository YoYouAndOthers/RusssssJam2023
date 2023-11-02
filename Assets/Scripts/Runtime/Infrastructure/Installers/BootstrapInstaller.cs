using RussSurvivor.Runtime.Application.Progress;
using RussSurvivor.Runtime.Application.Progress.Watchers;
using RussSurvivor.Runtime.Application.SaveLoad;
using RussSurvivor.Runtime.Infrastructure.Scenes;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace RussSurvivor.Runtime.Infrastructure.Installers
{
  public class BootstrapInstaller : MonoInstaller, IInitializable
  {
    [SerializeField] private Camera _camera;
    private ISceneLoader _sceneLoader;

    [Inject]
    private void Construct(ISceneLoader sceneLoader)
    {
      _sceneLoader = sceneLoader;
    }

    public async void Initialize()
    {
      Debug.Log("Bootstrap scene initializing");
      SceneEntrance.InitializedScene = SceneEntrance.SceneName.Bootstrap;
      await Container.Resolve<ILoadService>().LoadAsync();
      await _sceneLoader.LoadSceneAsync(SceneEntrance.SceneName.Battle, LoadSceneMode.Additive);
      Destroy(_camera.gameObject);
    }

    public override void InstallBindings()
    {
      Container
        .BindInterfacesTo<BootstrapInstaller>()
        .FromInstance(this)
        .AsSingle();

      Container
        .Bind<IPersistentProgress>()
        .To<PersistentProgress>()
        .FromNew()
        .AsSingle();

      Container
        .Bind<ILoadService>()
        .To<JsonLoadService>()
        .FromNew()
        .AsSingle();

      Container
        .Bind<ISaveService>()
        .To<JsonSaveService>()
        .FromNew()
        .AsSingle();

      Container
        .Bind<IProgressWatcherService>()
        .To<ProgressWatcherService>()
        .FromNew()
        .AsSingle();
    }
  }
}