using RussSurvivor.Runtime.Application.Progress;
using RussSurvivor.Runtime.Application.SaveLoad;
using UnityEngine;
using Zenject;

namespace RussSurvivor.Runtime.Infrastructure.Installers
{
  public class BattleInstaller : MonoInstaller, IInitializable
  {
    public void Initialize()
    {
      Debug.Log("Gameplay scene initializing");
      if (SceneEntrance.InitializedScene == SceneEntrance.SceneName.NotInitialized)
        InitializeAsInitialScene();
      else
        InitializeAsSubsequentScene();
    }

    public override void InstallBindings()
    {
      Container
        .BindInterfacesTo<BattleInstaller>()
        .FromInstance(this)
        .AsSingle();

      if (SceneEntrance.InitializedScene == SceneEntrance.SceneName.NotInitialized)
        InstallBindingsFromPassedScenes();
    }

    private void InitializeAsInitialScene()
    {
      Debug.Log("Gameplay scene initializing as initial scene");
    }

    private void InitializeAsSubsequentScene()
    {
      Debug.Log("Gameplay scene initializing as subsequent scene");
    }

    private void InstallBindingsFromPassedScenes()
    {
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