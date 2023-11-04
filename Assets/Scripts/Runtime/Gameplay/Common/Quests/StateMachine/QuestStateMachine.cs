using System;
using RussSurvivor.Runtime.Gameplay.Town.Characters;

namespace RussSurvivor.Runtime.Gameplay.Common.Quests.StateMachine
{
  public class QuestStateMachine : IQuestStateMachine
  {
    private readonly IActorRegistry _actorRegistry;
    public QuestState CurrentState { get; private set; }

    public QuestStateMachine(IActorRegistry actorRegistry) =>
      _actorRegistry = actorRegistry;

    public void InitializeAsNew(Guid initialNpcId)
    {
      CurrentState = new TalkToNpcQuestState(initialNpcId, _actorRegistry);
    }
  }
}