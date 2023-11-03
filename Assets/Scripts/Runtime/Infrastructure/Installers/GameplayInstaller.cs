using RussSurvivor.Runtime.Gameplay.Common.Cinema;
using RussSurvivor.Runtime.Gameplay.Common.Player;
using RussSurvivor.Runtime.Gameplay.Common.Transitions;
using UnityEngine;
using Zenject;

namespace RussSurvivor.Runtime.Infrastructure.Installers
{
  public class GameplayInstaller : MonoInstaller
  {
    [SerializeField] private CameraFollower _cameraFollower;

    public override void InstallBindings()
    {
      Container
        .Bind<IPlayerPrefabProvider>()
        .To<PlayerPrefabProvider>()
        .FromNew()
        .AsSingle();

      Container
        .Bind<GameplayInstaller>()
        .FromInstance(this)
        .AsSingle();

      Container
        .Bind<IGameplayTransitionService>()
        .To<GameplayTransitionService>()
        .FromNew()
        .AsSingle();
      
      Container
        .Bind<CameraFollower>()
        .FromInstance(_cameraFollower)
        .AsSingle();
    }
  }
}