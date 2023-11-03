using RussSurvivor.Runtime.Infrastructure.Scenes;
using RussSurvivor.Runtime.UI.MainMenu;
using UnityEngine;
using Zenject;

namespace RussSurvivor.Runtime.Infrastructure.Installers
{
  public class MainMenuInstaller : MonoInstaller, IInitializable
  {
    [SerializeField] private MainMenuUi _ui;
    private ICurtain _curtain;

    [Inject]
    private void Construct(ICurtain curtain)
    {
      _curtain = curtain;
    }

    public void Initialize()
    {
      _ui.Initialize();
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