using UnityEngine;
using Zenject;

namespace RussSurvivor.Runtime.Infrastructure.Installers
{
  public class ProjectInstaller : MonoInstaller, IInitializable
  {
    public void Initialize()
    {
      Debug.Log("Project initialized");
    }

    public override void InstallBindings()
    {
      Container
        .BindInterfacesTo<ProjectInstaller>()
        .FromInstance(this)
        .AsSingle();
    }
  }
}