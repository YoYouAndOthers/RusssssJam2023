using RussSurvivor.Runtime.Gameplay.Battle.Characters;
using RussSurvivor.Runtime.Gameplay.Common.Player;
using UnityEngine;
using Zenject;

namespace RussSurvivor.Runtime.Infrastructure.Installers
{
  public class TownInstaller : MonoInstaller, IInitializable
  {
    [SerializeField] private PlayerSpawnPoint _playerSpawnPoint;

    private GameplayInstaller _gameplayInstaller;

    [Inject]
    private void Construct(GameplayInstaller gameplayInstaller)
    {
      _gameplayInstaller = gameplayInstaller;
    }

    public async void Initialize()
    {
      await Container.Resolve<IPlayerPrefabProvider>().Initialize();
      _playerSpawnPoint.Initialize();
      _gameplayInstaller.Initialize();
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