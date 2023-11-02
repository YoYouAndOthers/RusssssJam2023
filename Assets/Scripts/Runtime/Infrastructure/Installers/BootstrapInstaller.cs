using RussSurvivor.Runtime.Infrastructure.Inputs;
using UnityEngine;
using Zenject;

namespace RussSurvivor.Runtime.Infrastructure.Installers
{
  public class BootstrapInstaller : MonoInstaller, IInitializable
  {
    public void Initialize()
    {
      Debug.Log("Bootstrap scene initializing");
    }

    [Inject] IInputService _inputService;
    
    private void Update()
    {
      Debug.Log(_inputService.GetMovementInput());
    }

    public override void InstallBindings()
    {
      Container
        .BindInterfacesTo<BootstrapInstaller>()
        .FromInstance(this)
        .AsSingle();
    }
  }
}