using Cysharp.Threading.Tasks;
using RussSurvivor.Runtime.Application.SaveLoad;
using RussSurvivor.Runtime.Gameplay.Battle.Characters;
using RussSurvivor.Runtime.Gameplay.Battle.Enemies;
using RussSurvivor.Runtime.Gameplay.Battle.Environment.Navigation;
using RussSurvivor.Runtime.Gameplay.Battle.Environment.Obstacles;
using RussSurvivor.Runtime.Gameplay.Battle.Quests;
using RussSurvivor.Runtime.Gameplay.Battle.States;
using RussSurvivor.Runtime.Gameplay.Battle.Timing;
using RussSurvivor.Runtime.Gameplay.Battle.Weapons;
using RussSurvivor.Runtime.Gameplay.Battle.Weapons.Target;
using RussSurvivor.Runtime.Gameplay.Common.Cinema;
using RussSurvivor.Runtime.Gameplay.Common.Player;
using RussSurvivor.Runtime.Gameplay.Common.Quests.Resolvers;
using RussSurvivor.Runtime.Gameplay.Common.Timing;
using RussSurvivor.Runtime.Gameplay.Common.Transitions;
using RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data;
using RussSurvivor.Runtime.Infrastructure.Scenes;
using RussSurvivor.Runtime.UI.Gameplay.Battle;
using RussSurvivor.Runtime.UI.Gameplay.Common.Quests;
using UnityEngine;
using Zenject;

namespace RussSurvivor.Runtime.Infrastructure.Installers
{
  public class BattleInstaller : MonoInstaller, IInitializable
  {
    [SerializeField] private PlayerSpawnPoint _playerSpawnPoint;
    [SerializeField] private NavMeshService _navMeshService;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private DestroyNpcQuestResolver _destroyNpcQuestResolver;

    private CameraFollower _cameraFollower;
    private CollectingQuestResolver _collectingQuestResolver;
    private CollectionQuestUi _collectionQuestUi;
    private ICooldownService _cooldownService;
    private ICurtain _curtain;
    private GameplayInstaller _gameplayInstaller;
    private IGameplayTransitionService _gameplayTransitionService;

    [Inject]
    private void Construct(
      ICurtain curtain,
      ICooldownService cooldownService,
      IConversationDataBase conversationDataBase,
      IGameplayTransitionService gameplayTransitionService,
      CameraFollower cameraFollower,
      CollectingQuestResolver collectingQuestResolver,
      CollectionQuestUi collectionQuestUi,
      GameplayInstaller gameplayInstaller)
    {
      _curtain = curtain;
      _cooldownService = cooldownService;
      _gameplayTransitionService = gameplayTransitionService;
      _cameraFollower = cameraFollower;
      _collectingQuestResolver = collectingQuestResolver;
      _collectionQuestUi = collectionQuestUi;
      _gameplayInstaller = gameplayInstaller;
    }

    private void OnDestroy()
    {
      Container.Resolve<ObstacleSpawner>().Dispose();
    }

    private void OnApplicationQuit()
    {
      Container.Resolve<ISaveService>().Save();
    }

    public async void Initialize()
    {
      await UniTask.WaitWhile(() => _gameplayInstaller.IsInitializing);
      await Container.Resolve<IDamageCountService>().InitializeAsync();

      _gameplayTransitionService.CurrentScene = SceneEntrance.SceneName.Battle;
      Debug.Log("Gameplay scene initializing");
      if (SceneEntrance.InitializedScene == SceneEntrance.SceneName.NotInitialized)
        InitializeAsInitialScene();
      else
        InitializeAsSubsequentScene();

      Container.Resolve<ClosestTargetPickerFactory>().Initialize();

      _playerSpawnPoint.Initialize();
      _destroyNpcQuestResolver.Initialize();
      Container.Resolve<ObstacleSpawner>().SpawnObstacles();
      _cameraFollower.Initialize(Container.Resolve<IPlayerRegistry>().GetPlayer());
      _collectingQuestResolver.Initialize();
      _collectionQuestUi.Initialize(_collectingQuestResolver);

      Container.Resolve<EnemySpawner>().Initialize();
      Container.Resolve<IPlayerWeaponService>().Initialize();
      _cooldownService.RegisterUpdatable(Container.Resolve<IPlayerWeaponService>());
      Container.Resolve<IBattleTimer>().Initialize();
      Container.Resolve<IBattleStateMachine>().SetState<MainBattleState>();

      _curtain.Hide();
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
        .Bind<IPlayerWeaponService>()
        .To<PlayerWeaponService>()
        .FromNew()
        .AsSingle();

      Container
        .Bind<IEnemyWeaponService>()
        .To<EnemyWeaponService>()
        .FromNew()
        .AsSingle();

      Container
        .Bind<IBattleStateMachine>()
        .To<BattleStateMachine>()
        .FromNew()
        .AsSingle();

      Container
        .Bind<IBattleTimer>()
        .To<BattleTimer>()
        .FromNew()
        .AsSingle();

      Container
        .Bind<IEnemyRegistry>()
        .To<EnemyRegistry>()
        .FromNew()
        .AsSingle();

      Container
        .Bind<INavMeshService>()
        .To<NavMeshService>()
        .FromInstance(_navMeshService)
        .AsSingle();

      Container
        .Bind<EnemySpawner>()
        .FromInstance(_enemySpawner)
        .AsSingle();

      Container
        .Bind<EnemyFactory>()
        .FromNew()
        .AsSingle();

      Container
        .Bind<IDamageCountService>()
        .To<DamageCountService>()
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