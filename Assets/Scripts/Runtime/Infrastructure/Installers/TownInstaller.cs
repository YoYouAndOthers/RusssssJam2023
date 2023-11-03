using RussSurvivor.Runtime.Gameplay.Battle.Characters;
using RussSurvivor.Runtime.Gameplay.Common.Cinema;
using RussSurvivor.Runtime.Gameplay.Common.Player;
using RussSurvivor.Runtime.Infrastructure.Scenes;
using UnityEngine;
using Zenject;

namespace RussSurvivor.Runtime.Infrastructure.Installers
{
  public class TownInstaller : MonoInstaller, IInitializable
  {
    [SerializeField] private PlayerSpawnPoint _playerSpawnPoint;

    private ICurtain _curtain;
    private CameraFollower _cameraFollower;

    [Inject]
    private void Construct(ICurtain curtain, CameraFollower cameraFollower)
    {
      _curtain = curtain;
      _cameraFollower = cameraFollower;
    }

    public async void Initialize()
    {
      await Container.Resolve<IPlayerPrefabProvider>().Initialize();
      _playerSpawnPoint.Initialize();
      _cameraFollower.Initialize(Container.Resolve<IPlayerRegistry>().GetPlayer());
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