using RussSurvivor.Runtime.Application.Progress;
using RussSurvivor.Runtime.Application.Progress.Watchers;
using RussSurvivor.Runtime.Application.SaveLoad;
using RussSurvivor.Runtime.Gameplay.Battle.Characters;
using RussSurvivor.Runtime.Gameplay.Battle.Environment.Obstacles;
using RussSurvivor.Runtime.Gameplay.Battle.Timing;
using RussSurvivor.Runtime.Gameplay.Battle.Weapons;
using RussSurvivor.Runtime.Gameplay.Battle.Weapons.Target;
using RussSurvivor.Runtime.Gameplay.Common.Cinema;
using UnityEngine;
using Zenject;

namespace RussSurvivor.Runtime.Infrastructure.Installers
{
  public class BattleInstaller : MonoInstaller, IInitializable, ITickable
  {
    [SerializeField] private PlayerBattleSpawnPoint _playerSpawnPoint;
    [SerializeField] private CameraFollower _cameraFollower;
    private ICooldownService _cooldownService;

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
      await _playerSpawnPoint.Initialize();
      _cameraFollower.Initialize();
      Container.Resolve<ObstacleSpawner>().SpawnObstacles();
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
        .Bind<IPersistentProgress>()
        .To<PersistentProgress>()
        .FromNew()
        .AsSingle();

      Container
        .Bind<ILoadService>()
        .To<JsonLoadService>()
        .FromNew()
        .AsSingle();

      Container
        .Bind<ISaveService>()
        .To<JsonSaveService>()
        .FromNew()
        .AsSingle();

      Container
        .Bind<IProgressWatcherService>()
        .To<ProgressWatcherService>()
        .FromNew()
        .AsSingle();

      Container
        .Bind<ICharacterRegistry>()
        .To<CharacterRegistry>()
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