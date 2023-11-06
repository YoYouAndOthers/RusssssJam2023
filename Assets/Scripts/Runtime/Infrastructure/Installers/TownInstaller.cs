using Cysharp.Threading.Tasks;
using RussSurvivor.Runtime.Gameplay.Battle.Characters;
using RussSurvivor.Runtime.Gameplay.Battle.Weapons.Content;
using RussSurvivor.Runtime.Gameplay.Common.Cinema;
using RussSurvivor.Runtime.Gameplay.Common.Player;
using RussSurvivor.Runtime.Gameplay.Common.Quests;
using RussSurvivor.Runtime.Gameplay.Common.Quests.Data;
using RussSurvivor.Runtime.Gameplay.Common.Quests.Resolvers;
using RussSurvivor.Runtime.Gameplay.Common.Quests.StateMachine;
using RussSurvivor.Runtime.Gameplay.Common.Timing;
using RussSurvivor.Runtime.Gameplay.Common.Transitions;
using RussSurvivor.Runtime.Gameplay.Town.Dialogues;
using RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data;
using RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data.Actions;
using RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data.Conditions;
using RussSurvivor.Runtime.Gameplay.Town.Economics.Trade;
using RussSurvivor.Runtime.Infrastructure.Content;
using RussSurvivor.Runtime.Infrastructure.Scenes;
using RussSurvivor.Runtime.UI.Gameplay.Common.Quests;
using RussSurvivor.Runtime.UI.Gameplay.Town.Dialogues;
using RussSurvivor.Runtime.UI.Gameplay.Town.Trade;
using UniRx;
using UnityEngine;
using Zenject;

namespace RussSurvivor.Runtime.Infrastructure.Installers
{
  public class TownInstaller : MonoInstaller, IInitializable
  {
    [SerializeField] private PlayerSpawnPoint _playerSpawnPoint;
    [SerializeField] private QuestConfig _initialQuestConfig;
    [SerializeField] private DialogueEntryPresenter _dialogueEntryPresenter;
    [SerializeField] private TradeUiPresenter _tradeUiPresenter;

    private CameraFollower _cameraFollower;
    private CollectingQuestResolver _collectingQuestResolver;
    private CollectionQuestUi _collectionQuestUi;
    private IConversationDataBase _conversationDataBase;
    private ICooldownService _cooldownService;
    private ICurtain _curtain;
    private IDayTimer _dayTimer;
    private IGameplayTransitionService _gameplayTransitionService;
    private IQuestStateMachine _questStateMachine;
    private IWeaponConfigProvider _weaponConfigProvider;

    [Inject]
    private void Construct(
      ICurtain curtain,
      IDayTimer dayTimer,
      ICooldownService cooldownService,
      IWeaponConfigProvider weaponConfigProvider,
      IConversationDataBase conversationDataBase,
      IGameplayTransitionService gameplayTransitionService,
      CameraFollower cameraFollower,
      IQuestStateMachine questStateMachine,
      CollectingQuestResolver collectingQuestResolver,
      CollectionQuestUi collectionQuestUi)
    {
      _curtain = curtain;
      _dayTimer = dayTimer;
      _cooldownService = cooldownService;
      _weaponConfigProvider = weaponConfigProvider;
      _conversationDataBase = conversationDataBase;
      _gameplayTransitionService = gameplayTransitionService;
      _cameraFollower = cameraFollower;
      _questStateMachine = questStateMachine;
      _collectingQuestResolver = collectingQuestResolver;
      _collectionQuestUi = collectionQuestUi;
    }

    public async void Initialize()
    {
      _gameplayTransitionService.CurrentScene = SceneEntrance.SceneName.Town;
      await UniTask.WhenAll(
        Container.Resolve<IPlayerPrefabProvider>().InitializeAsync(),
        Container.Resolve<IQuestRegistry>().InitializeAsync(),
        Container.Resolve<ICollectableItemPrefabProvider>().InitializeAsync());

      if (_questStateMachine.CurrentState.Value == null)
        _questStateMachine.StartNewQuest(_initialQuestConfig.Id);
      _playerSpawnPoint.Initialize();
      _cameraFollower.Initialize(Container.Resolve<IPlayerRegistry>().GetPlayer());
      _dialogueEntryPresenter.Initialize();
      _collectingQuestResolver.Initialize();
      _collectionQuestUi.Initialize(_collectingQuestResolver);
      Container.Resolve<ITraderService>().Initialize();
      _tradeUiPresenter.Initialize();

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

      Container
        .Bind<IDialogueSystem>()
        .To<DialogueSystem>()
        .FromNew()
        .AsSingle();

      Container
        .Bind<IConversationConditionSolver>()
        .To<ConversationConditionSolver>()
        .FromNew()
        .AsSingle();

      Container
        .Bind<IConversationActionInvoker>()
        .To<ConversationActionInvoker>()
        .FromNew()
        .AsSingle();

      Container
        .Bind<ITraderService>()
        .To<TraderService>()
        .FromNew()
        .AsSingle();
    }
  }
}