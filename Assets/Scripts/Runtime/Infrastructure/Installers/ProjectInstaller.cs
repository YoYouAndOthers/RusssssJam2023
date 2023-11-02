using RussSurvivor.Runtime.Infrastructure.Inputs;
using UnityEngine;
using Zenject;

namespace RussSurvivor.Runtime.Infrastructure.Installers
{
  public class ProjectInstaller : MonoInstaller, IInitializable
  {
    public void Initialize()
    {
      Debug.Log("Project initialized");
      Container.Resolve<IInputService>().Initialize();
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
    }
  }
}