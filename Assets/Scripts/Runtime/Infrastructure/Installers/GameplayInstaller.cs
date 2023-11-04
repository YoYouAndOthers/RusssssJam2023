using RussSurvivor.Runtime.Gameplay.Common.Cinema;
using RussSurvivor.Runtime.Gameplay.Common.Player;
using RussSurvivor.Runtime.Gameplay.Common.Quests;
using RussSurvivor.Runtime.Gameplay.Common.Quests.StateMachine;
using RussSurvivor.Runtime.Gameplay.Common.Transitions;
using RussSurvivor.Runtime.Gameplay.Town.Characters;
using RussSurvivor.Runtime.Infrastructure.Content;
using UnityEngine;
using Zenject;

namespace RussSurvivor.Runtime.Infrastructure.Installers
{
  public class GameplayInstaller : MonoInstaller
  {
    [SerializeField] private CameraFollower _cameraFollower;

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
    }
  }
}