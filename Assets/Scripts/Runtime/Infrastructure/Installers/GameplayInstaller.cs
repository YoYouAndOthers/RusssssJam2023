using RussSurvivor.Runtime.Gameplay.Battle.Timing;
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
using RussSurvivor.Runtime.UI.Gameplay.Common.Quests;
using UnityEngine;
using Zenject;

namespace RussSurvivor.Runtime.Infrastructure.Installers
{
  public class GameplayInstaller : MonoInstaller, ITickable
  {
    [SerializeField] private CameraFollower _cameraFollower;
    [SerializeField] private CollectionQuestUi _collectionQuestUi;
    [SerializeField] private CollectingQuestResolver _collectingQuestResolver;
    private ICooldownService _cooldownService;

    public void Tick()
    {
      _cooldownService ??= Container.Resolve<ICooldownService>();
      _cooldownService.PerformTick(Time.deltaTime);
    }

    public override void InstallBindings()
    {
      Container
        .BindInterfacesTo<GameplayInstaller>()
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
    }
  }
}