using QFSW.QC;
using RussSurvivor.Runtime.Application;
using RussSurvivor.Runtime.Application.Progress;
using RussSurvivor.Runtime.Application.Progress.Watchers;
using RussSurvivor.Runtime.Application.SaveLoad;
using RussSurvivor.Runtime.Infrastructure.Inputs;
using RussSurvivor.Runtime.Infrastructure.Logging;
using RussSurvivor.Runtime.Infrastructure.Scenes;
using UnityEngine;
using Zenject;

namespace RussSurvivor.Runtime.Infrastructure.Installers
{
  public class ProjectInstaller : MonoInstaller, IInitializable
  {
    [SerializeField] private QuantumConsole _consolePrefab;

    public void Initialize()
    {
      Debug.Log("Project initialized");
      Container.Resolve<IInputService>().Initialize();
      Container.Resolve<IDebugService>()
        .Initialize(Container.InstantiatePrefabForComponent<QuantumConsole>(_consolePrefab));
    }

    public override void InstallBindings()
    {
      Container
        .BindInterfacesTo<ProjectInstaller>()
        .FromInstance(this)
        .AsSingle();

      Container
        .Bind<IInputService>()
        .To<InputService>()
        .FromNew()
        .AsSingle();

      Container
        .Bind<ISceneLoader>()
        .To<SceneLoader>()
        .FromNew()
        .AsSingle();

      Container
        .Bind<IDebugService>()
        .To<DebugService>()
        .FromNew()
        .AsSingle();

      Container
        .Bind<ICurtain>()
        .To<Curtain>()
        .FromComponentInNewPrefabResource("Prefabs/UI/LoadingCurtain")
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

      Container
        .Bind<IApplicationService>()
        .To<ApplicationService>()
        .FromNew()
        .AsSingle();
    }
  }
}