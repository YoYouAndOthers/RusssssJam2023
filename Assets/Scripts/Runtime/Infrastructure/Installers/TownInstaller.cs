using RussSurvivor.Runtime.Gameplay.Battle.Characters;
using RussSurvivor.Runtime.Gameplay.Common.Player;
using RussSurvivor.Runtime.Infrastructure.Scenes;
using UnityEngine;
using Zenject;

namespace RussSurvivor.Runtime.Infrastructure.Installers
{
  public class TownInstaller : MonoInstaller, IInitializable
  {
    [SerializeField] private PlayerSpawnPoint _playerSpawnPoint;

    private GameplayInstaller _gameplayInstaller;
    private ICurtain _curtain;

    [Inject]
    private void Construct(GameplayInstaller gameplayInstaller, ICurtain curtain)
    {
      _gameplayInstaller = gameplayInstaller;
      _curtain = curtain;
    }

    public async void Initialize()
    {
      await Container.Resolve<IPlayerPrefabProvider>().Initialize();
      _playerSpawnPoint.Initialize();
      _gameplayInstaller.Initialize();
      _curtain.Hide();
    }

    public override void InstallBindings()
    {
      Container
        .BindInterfacesTo<TownInstaller>()
        .FromInstance(this)
        .AsSingle();

      Container
        .Bind(typeof(IPlayerRegistry), typeof(ITownPlayerRegistry))
        .To<TownPlayerRegistry>()
        .FromNew()
        .AsSingle();
    }
  }
}