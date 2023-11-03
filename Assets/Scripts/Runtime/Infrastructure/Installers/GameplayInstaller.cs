using RussSurvivor.Runtime.Gameplay.Common.Cinema;
using RussSurvivor.Runtime.Gameplay.Common.Player;
using UnityEngine;
using Zenject;

namespace RussSurvivor.Runtime.Infrastructure.Installers
{
  public class GameplayInstaller : MonoInstaller, IInitializable
  {
    [SerializeField] private CameraFollower _cameraFollower;

    public void Initialize()
    {
      Debug.Log("Gameplay scene initializing");
      _cameraFollower.Initialize();
    }

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
    }
  }
}