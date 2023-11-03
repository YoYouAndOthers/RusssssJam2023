using RussSurvivor.Runtime.Infrastructure.Scenes;
using Zenject;

namespace RussSurvivor.Runtime.Infrastructure.Installers
{
  public class MainMenuInstaller : MonoInstaller, IInitializable
  {
    private ICurtain _curtain;

    [Inject]
    private void Construct(ICurtain curtain)
    {
      _curtain = curtain;
    }

    public void Initialize()
    {
      _curtain.Hide();
    }

    public override void InstallBindings()
    {
      Container
        .BindInterfacesTo<MainMenuInstaller>()
        .FromInstance(this)
        .AsSingle();
    }
  }
}