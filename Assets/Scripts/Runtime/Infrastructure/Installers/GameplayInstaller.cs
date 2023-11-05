using RussSurvivor.Runtime.Gameplay.Common.Cinema;
using RussSurvivor.Runtime.Gameplay.Common.Player;
using RussSurvivor.Runtime.Gameplay.Common.Quests;
using RussSurvivor.Runtime.Gameplay.Common.Quests.Resolvers;
using RussSurvivor.Runtime.Gameplay.Common.Quests.StateMachine;
using RussSurvivor.Runtime.Gameplay.Common.Transitions;
using RussSurvivor.Runtime.Gameplay.Town.Characters;
using RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data;
using RussSurvivor.Runtime.Infrastructure.Content;
using RussSurvivor.Runtime.UI.Gameplay.Common.Quests;
using UnityEngine;
using Zenject;

namespace RussSurvivor.Runtime.Infrastructure.Installers
{
  public class GameplayInstaller : MonoInstaller
  {
    [SerializeField] private CameraFollower _cameraFollower;
    [SerializeField] private CollectionQuestUi _collectionQuestUi;
    [SerializeField] private CollectingQuestResolver _collectingQuestResolver;

    public override void InstallBindings()
    {
      Container
        .Bind<IPlayerPrefabProvider>()
        .To<PlayerPrefabProvider>()
        .FromNew()
        .AsSingle();

      Container
        .Bind<GameplayInstaller>()
        .FromInstance(this)
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
    }
  }
}