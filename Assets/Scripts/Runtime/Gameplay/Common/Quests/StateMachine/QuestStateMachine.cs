using System;
using RussSurvivor.Runtime.Gameplay.Town.Characters;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Common.Quests.StateMachine
{
  public class QuestStateMachine : IQuestStateMachine
  {
    private readonly IActorRegistry _actorRegistry;
    public QuestState CurrentState { get; private set; }

    public QuestStateMachine(IActorRegistry actorRegistry) =>
      _actorRegistry = actorRegistry;

    public void InitializeAsNew(Guid questId, Guid initialNpcId)
    {
      CurrentState = new TalkToNpcQuestState(questId, initialNpcId, _actorRegistry);
    }

    public void StartNewQuest(Guid questIs)
    {
      Debug.Log($"Starting new quest {questIs}");
    }

    public void CompleteCurrentQuest()
    {
      Debug.Log($"Completing quest {CurrentState.QuestId}");
      CurrentState = null;
    }
  }
}