using Cysharp.Threading.Tasks;
using RussSurvivor.Runtime.Gameplay.Battle.Characters;
using RussSurvivor.Runtime.Gameplay.Battle.Timing;
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
using RussSurvivor.Runtime.Infrastructure.Content;
using RussSurvivor.Runtime.Infrastructure.Scenes;
using RussSurvivor.Runtime.UI.Gameplay.Common.Quests;
using RussSurvivor.Runtime.UI.Gameplay.Town.Dialogues;
using UnityEngine;
using Zenject;

namespace RussSurvivor.Runtime.Infrastructure.Installers
{
  public class TownInstaller : MonoInstaller, IInitializable
  {
    [SerializeField] private PlayerSpawnPoint _playerSpawnPoint;
    [SerializeField] private QuestConfig _initialQuestConfig;
    [SerializeField] private DialogueEntryPresenter _dialogueEntryPresenter;
    private CameraFollower _cameraFollower;
    private CollectionQuestUi _collectionQuestUi;
    private ICurtain _curtain;
    private IGameplayTransitionService _gameplayTransitionService;
    private CollectingQuestResolver _collectingQuestResolver;
    private IQuestStateMachine _questStateMachine;
    private IConversationDataBase _conversationDataBase;
    private IDayTimer _dayTimer;
    private ICooldownService _cooldownService;

    [Inject]
    private void Construct(
      ICurtain curtain,
      IDayTimer dayTimer,
      ICooldownService cooldownService,
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
        _conversationDataBase.InitializeAsync(),
        Container.Resolve<IPlayerPrefabProvider>().InitializeAsync(),
        Container.Resolve<IQuestRegistry>().InitializeAsync(),
        Container.Resolve<ICollectableItemPrefabProvider>().InitializeAsync());

      if (!_dayTimer.IsRunning)
        _cooldownService.RegisterUpdatable(_dayTimer);

      if (_questStateMachine.CurrentState.Value == null)
        _questStateMachine.StartNewQuest(_initialQuestConfig.Id);
      _playerSpawnPoint.Initialize();
      _cameraFollower.Initialize(Container.Resolve<IPlayerRegistry>().GetPlayer());
      _dialogueEntryPresenter.Initialize();
      _collectingQuestResolver.Initialize();
      _collectionQuestUi.Initialize(_collectingQuestResolver);
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
    }
  }
}