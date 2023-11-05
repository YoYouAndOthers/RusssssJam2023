using Cysharp.Threading.Tasks;
using RussSurvivor.Runtime.Application.SaveLoad;
using RussSurvivor.Runtime.Gameplay.Battle.Enemies;
using RussSurvivor.Runtime.Gameplay.Battle.Weapons.Content;
using RussSurvivor.Runtime.Gameplay.Battle.Weapons.Registry;
using RussSurvivor.Runtime.Gameplay.Common.Cinema;
using RussSurvivor.Runtime.Gameplay.Common.Player;
using RussSurvivor.Runtime.Gameplay.Common.Quests;
using RussSurvivor.Runtime.Gameplay.Common.Quests.Resolvers;
using RussSurvivor.Runtime.Gameplay.Common.Quests.StateMachine;
using RussSurvivor.Runtime.Gameplay.Common.Timing;
using RussSurvivor.Runtime.Gameplay.Common.Transitions;
using RussSurvivor.Runtime.Gameplay.Town.Characters;
using RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data;
using RussSurvivor.Runtime.Infrastructure.Content;
using RussSurvivor.Runtime.Infrastructure.Scenes;
using RussSurvivor.Runtime.UI.Gameplay.Common.Quests;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace RussSurvivor.Runtime.Infrastructure.Installers
{
  public class GameplayInstaller : MonoInstaller, IInitializable, ITickable
  {
    [SerializeField] private CameraFollower _cameraFollower;
    [SerializeField] private CollectionQuestUi _collectionQuestUi;
    [SerializeField] private CollectingQuestResolver _collectingQuestResolver;
    private ICooldownService _cooldownService;
    private ISceneLoader _sceneLoader;

    public bool IsInitializing { get; private set; }

    [Inject]
    private void Construct(ISceneLoader sceneLoader)
    {
      _sceneLoader = sceneLoader;
    }

    public async void Initialize()
    {
      IsInitializing = true;
      await UniTask.WhenAll(
        Container.Resolve<ILoadService>().LoadAsync(),
        Container.Resolve<IPlayerPrefabProvider>().InitializeAsync(),
        Container.Resolve<IQuestRegistry>().InitializeAsync(),
        Container.Resolve<ICollectableItemPrefabProvider>().InitializeAsync(),
        Container.Resolve<IConversationDataBase>().InitializeAsync(),
        Container.Resolve<IEnemyTypeStaticProvider>().InitializeAsync(),
        Container.Resolve<IWeaponConfigProvider>().InitializeAsync()
      );

      Container.Resolve<ICooldownService>().RegisterUpdatable(Container.Resolve<IDayTimer>());

      await LoadTownSceneIfNeeded();
      IsInitializing = false;
    }

    private async UniTask LoadTownSceneIfNeeded()
    {
      string townSceneName = _sceneLoader.GetSceneName(SceneEntrance.SceneName.Town);
      string battleSceneName = _sceneLoader.GetSceneName(SceneEntrance.SceneName.Battle);
      if (SceneManager.GetSceneByName(townSceneName).isLoaded || SceneManager.GetSceneByName(battleSceneName).isLoaded)
        return;
      await _sceneLoader.LoadSceneAsync(SceneEntrance.SceneName.Town, LoadSceneMode.Additive);
    }

    public void Tick()
    {
      _cooldownService ??= Container.Resolve<ICooldownService>();
      _cooldownService.PerformTick(Time.deltaTime);
    }

    public override void InstallBindings()
    {
      Container
        .BindInterfacesAndSelfTo<GameplayInstaller>()
        .FromInstance(this)
        .AsSingle();

      Container
        .Bind<IPlayerPrefabProvider>()
        .To<PlayerPrefabProvider>()
        .FromNew()
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

      Container
        .Bind<IActorRegistry>()
        .To<ActorRegistry>()
        .FromNew()
        .AsSingle();

      Container
        .Bind<IQuestStateMachine>()
        .To<QuestStateMachine>()
        .FromNew()
        .AsSingle();

      Container
        .Bind<IQuestRegistry>()
        .To<QuestRegistry>()
        .FromNew()
        .AsSingle();

      Container
        .Bind<IQuestStateListFactory>()
        .To<QuestStateListFactory>()
        .FromNew()
        .AsSingle();

      Container
        .Bind<ICollectableItemPrefabProvider>()
        .To<CollectableItemPrefabProvider>()
        .FromNew()
        .AsSingle();

      Container
        .Bind<CollectionQuestUi>()
        .FromInstance(_collectionQuestUi)
        .AsSingle();

      Container
        .Bind<CollectingQuestResolver>()
        .FromInstance(_collectingQuestResolver)
        .AsSingle();

      Container
        .Bind<IConversationDataBase>()
        .To<ConversationDataBase>()
        .FromNew()
        .AsSingle();

      Container
        .Bind<ICooldownService>()
        .To<CooldownService>()
        .FromNew()
        .AsSingle();

      Container
        .Bind<IDayTimer>()
        .To<DayTimer>()
        .FromNew()
        .AsSingle();

      Container
        .Bind<IWeaponConfigProvider>()
        .To<WeaponConfigProvider>()
        .FromNew()
        .AsSingle();

      Container
        .Bind<IEnemyTypeStaticProvider>()
        .To<EnemyTypeStaticProvider>()
        .FromNew()
        .AsSingle();

      Container
        .Bind<IWeaponRegistry>()
        .To<WeaponRegistry>()
        .FromNew()
        .AsSingle();
    }
  }
}