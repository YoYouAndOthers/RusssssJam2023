using Cysharp.Threading.Tasks;
using RussSurvivor.Runtime.Application.Progress;
using RussSurvivor.Runtime.Application.SaveLoad;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace RussSurvivor.Runtime.Infrastructure.Installers
{
  public class BootstrapInstaller : MonoInstaller, IInitializable
  {
    public async void Initialize()
    {
      Debug.Log("Bootstrap scene initializing");
      SceneEntrance.InitializedScene = SceneEntrance.SceneName.Bootstrap;
      await Container.Resolve<ILoadService>().LoadAsync();
      await SceneManager.LoadSceneAsync("Gameplay", LoadSceneMode.Single);
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
    }
  }
}