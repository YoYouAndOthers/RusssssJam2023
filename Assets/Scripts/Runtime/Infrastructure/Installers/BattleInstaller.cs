using RussSurvivor.Runtime.Application.SaveLoad;
using RussSurvivor.Runtime.Gameplay.Battle.Characters;
using RussSurvivor.Runtime.Gameplay.Battle.Environment.Obstacles;
using RussSurvivor.Runtime.Gameplay.Battle.Timing;
using RussSurvivor.Runtime.Gameplay.Battle.Weapons;
using RussSurvivor.Runtime.Gameplay.Battle.Weapons.Target;
using RussSurvivor.Runtime.Gameplay.Common.Player;
using UnityEngine;
using Zenject;

namespace RussSurvivor.Runtime.Infrastructure.Installers
{
  public class BattleInstaller : MonoInstaller, IInitializable, ITickable
  {
    [SerializeField] private PlayerSpawnPoint _playerSpawnPoint;
    private ICooldownService _cooldownService;
    private GameplayInstaller _gameplayInstaller;

    private void OnApplicationQuit()
    {
      Container.Resolve<ISaveService>().Save();
    }

    [Inject]
    private void Construct(GameplayInstaller gameplayInstaller)
    {
      _gameplayInstaller = gameplayInstaller;
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
      await Container.Resolve<IPlayerPrefabProvider>().Initialize();
      _playerSpawnPoint.Initialize();
      Container.Resolve<ObstacleSpawner>().SpawnObstacles();
      _gameplayInstaller.Initialize();
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