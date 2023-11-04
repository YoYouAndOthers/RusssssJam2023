using RussSurvivor.Runtime.Application.SaveLoad;
using RussSurvivor.Runtime.Gameplay.Battle.Characters;
using RussSurvivor.Runtime.Gameplay.Battle.Environment.Obstacles;
using RussSurvivor.Runtime.Gameplay.Battle.Timing;
using RussSurvivor.Runtime.Gameplay.Battle.Weapons;
using RussSurvivor.Runtime.Gameplay.Battle.Weapons.Target;
using RussSurvivor.Runtime.Gameplay.Common.Cinema;
using RussSurvivor.Runtime.Gameplay.Common.Player;
using RussSurvivor.Runtime.Infrastructure.Scenes;
using UnityEngine;
using Zenject;

namespace RussSurvivor.Runtime.Infrastructure.Installers
{
  public class BattleInstaller : MonoInstaller, IInitializable, ITickable
  {
    [SerializeField] private PlayerSpawnPoint _playerSpawnPoint;
    private CameraFollower _cameraFollower;
    private ICooldownService _cooldownService;
    private ICurtain _curtain;

    [Inject]
    private void Construct(ICurtain curtain, CameraFollower cameraFollower)
    {
      _curtain = curtain;
      _cameraFollower = cameraFollower;
    }

    private void OnApplicationQuit()
    {
      Container.Resolve<ISaveService>().Save();
    }

    public async void Initialize()
    {
      Debug.Log("Gameplay scene initializing");
      if (SceneEntrance.InitializedScene == SceneEntrance.SceneName.NotInitialized)
        InitializeAsInitialScene();
      else
        InitializeAsSubsequentScene();
      Container.Resolve<ClosestTargetPickerFactory>().Initialize();

      await Container.Resolve<ILoadService>().LoadAsync();
      await Container.Resolve<IPlayerPrefabProvider>().InitializeAsync();
      _playerSpawnPoint.Initialize();
      Container.Resolve<ObstacleSpawner>().SpawnObstacles();
      _cameraFollower.Initialize(Container.Resolve<IPlayerRegistry>().GetPlayer());
      _curtain.Hide();
    }

    public void Tick()
    {
      _cooldownService ??= Container.Resolve<ICooldownService>();
      _cooldownService.PerformTick(Time.deltaTime);
    }

    public override void InstallBindings()
    {
      Container
        .BindInterfacesTo<BattleInstaller>()
        .FromInstance(this)
        .AsSingle();

      Container
        .Bind(typeof(IPlayerRegistry), typeof(IBattlePlayerRegistry))
        .To<BattlePlayerRegistry>()
        .FromNew()
        .AsSingle();

      Container
        .Bind<ObstacleSpawner>()
        .FromNew()
        .AsSingle();

      Container
        .BindFactory<Transform, ClosestTargetPicker, ClosestTargetPickerFactory>()
        .AsSingle();

      Container
        .Bind<WeaponFactory>()
        .FromNew()
        .AsSingle();

      Container
        .Bind<ICooldownService>()
        .To<CooldownService>()
        .FromNew()
        .AsSingle();

      Container
        .Bind<IPlayerWeaponService>()
        .To<PlayerWeaponService>()
        .FromNew()
        .AsSingle();

      Container
        .Bind<IEnemyWeaponService>()
        .To<EnemyWeaponService>()
        .FromNew()
        .AsSingle();
    }

    private void InitializeAsInitialScene()
    {
      Debug.Log("Gameplay scene initializing as initial scene");
    }

    private void InitializeAsSubsequentScene()
    {
      Debug.Log("Gameplay scene initializing as subsequent scene");
    }
  }
}