using Cysharp.Threading.Tasks;
using RussSurvivor.Runtime.Application.SaveLoad;
using RussSurvivor.Runtime.Gameplay.Battle.Characters;
using RussSurvivor.Runtime.Gameplay.Battle.Environment.Obstacles;
using RussSurvivor.Runtime.Gameplay.Battle.Timing;
using RussSurvivor.Runtime.Gameplay.Battle.Weapons;
using RussSurvivor.Runtime.Gameplay.Battle.Weapons.Target;
using RussSurvivor.Runtime.Gameplay.Common.Cinema;
using RussSurvivor.Runtime.Gameplay.Common.Player;
using RussSurvivor.Runtime.Gameplay.Common.Quests.Resolvers;
using RussSurvivor.Runtime.Infrastructure.Scenes;
using RussSurvivor.Runtime.UI.Gameplay.Common.Quests;
using UnityEngine;
using Zenject;

namespace RussSurvivor.Runtime.Infrastructure.Installers
{
  public class BattleInstaller : MonoInstaller, IInitializable, ITickable
  {
    [SerializeField] private PlayerSpawnPoint _playerSpawnPoint;
    [SerializeField] private CollectingQuestResolver _collectingQuestResolver;
    private ICooldownService _cooldownService;
    private ICurtain _curtain;
    private CameraFollower _cameraFollower;
    private CollectionQuestUi _collectionQuestUi;

    [Inject]
    private void Construct(ICurtain curtain, CameraFollower cameraFollower, CollectionQuestUi collectionQuestUi)
    {
      _curtain = curtain;
      _cameraFollower = cameraFollower;
      _collectionQuestUi = collectionQuestUi;
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

      await UniTask.WhenAll(
        Container.Resolve<ILoadService>().LoadAsync(),
        Container.Resolve<IPlayerPrefabProvider>().InitializeAsync()
      );

      _collectingQuestResolver.Initialize();
      _playerSpawnPoint.Initialize();
      Container.Resolve<ObstacleSpawner>().SpawnObstacles();
      _cameraFollower.Initialize(Container.Resolve<IPlayerRegistry>().GetPlayer());
      _collectionQuestUi.Initialize(_collectingQuestResolver);
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