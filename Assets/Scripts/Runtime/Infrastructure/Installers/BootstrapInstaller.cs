using RussSurvivor.Runtime.Application.Progress;
using RussSurvivor.Runtime.Application.SaveLoad;
using RussSurvivor.Runtime.Infrastructure.Inputs;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace RussSurvivor.Runtime.Infrastructure.Installers
{
  public class BootstrapInstaller : MonoInstaller, IInitializable
  {
    public void Initialize()
    {
      Debug.Log("Bootstrap scene initializing");
      SceneEntrance.InitializedScene = SceneEntrance.SceneName.Bootstrap;
      SceneManager.LoadScene("Gameplay", LoadSceneMode.Single);
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