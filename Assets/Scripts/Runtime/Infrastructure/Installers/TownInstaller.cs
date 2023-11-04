using Cysharp.Threading.Tasks;
using RussSurvivor.Runtime.Gameplay.Battle.Characters;
using RussSurvivor.Runtime.Gameplay.Common.Cinema;
using RussSurvivor.Runtime.Gameplay.Common.Player;
using RussSurvivor.Runtime.Gameplay.Common.Quests;
using RussSurvivor.Runtime.Gameplay.Common.Quests.Data;
using RussSurvivor.Runtime.Gameplay.Common.Quests.Resolvers;
using RussSurvivor.Runtime.Gameplay.Common.Quests.StateMachine;
using RussSurvivor.Runtime.Gameplay.Town.Dialogues;
using RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data;
using RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data.Actions;
using RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data.Conditions;
using RussSurvivor.Runtime.Infrastructure.Content;
using RussSurvivor.Runtime.Infrastructure.Scenes;
using RussSurvivor.Runtime.UI.Gameplay.Town.Dialogues;
using UniRx;
using UnityEngine;
using Zenject;

namespace RussSurvivor.Runtime.Infrastructure.Installers
{
  public class TownInstaller : MonoInstaller, IInitializable
  {
    [SerializeField] private PlayerSpawnPoint _playerSpawnPoint;
    [SerializeField] private Actor _initialQuestGiver;
    [SerializeField] private QuestConfig _initialQuestConfig;
    [SerializeField] private DialogueEntryPresenter _dialogueEntryPresenter;
    [SerializeField] private CollectingQuestResolver _collectingQuestResolver;
    private CameraFollower _cameraFollower;

    private ICurtain _curtain;
    private IQuestStateMachine _questStateMachine;

    [Inject]
    private void Construct(ICurtain curtain, CameraFollower cameraFollower, IQuestStateMachine questStateMachine)
    {
      _curtain = curtain;
      _cameraFollower = cameraFollower;
      _questStateMachine = questStateMachine;
    }

    public async void Initialize()
    {
      await UniTask.WhenAll(
        Container.Resolve<IConversationDataBase>().InitializeAsync(),
        Container.Resolve<IPlayerPrefabProvider>().InitializeAsync(),
        Container.Resolve<IQuestRegistry>().InitializeAsync(),
        Container.Resolve<ICollectableItemPrefabProvider>().InitializeAsync());
      _questStateMachine.InitializeAsNew(_initialQuestConfig.Id, _initialQuestGiver.Id);
      _playerSpawnPoint.Initialize();
      _cameraFollower.Initialize(Container.Resolve<IPlayerRegistry>().GetPlayer());
      _dialogueEntryPresenter.Initialize();
      _collectingQuestResolver.Initialize();
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
        .Bind<IConversationDataBase>()
        .To<ConversationDataBase>()
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