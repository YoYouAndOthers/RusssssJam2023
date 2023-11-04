using System;
using System.Collections.Generic;
using RussSurvivor.Runtime.Gameplay.Town.Characters;
using UniRx;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Common.Quests.StateMachine
{
  public class QuestStateMachine : IQuestStateMachine
  {
    private readonly IQuestRegistry _questRegistry;
    private readonly IActorRegistry _actorRegistry;
    private readonly IQuestStateListFactory _questStateListFactory;
    public IReactiveProperty<QuestState> CurrentState { get; } = new ReactiveProperty<QuestState>();
    private List<QuestState> States { get; set; } = new();

    public QuestStateMachine(IQuestRegistry questRegistry, IActorRegistry actorRegistry,
      IQuestStateListFactory questStateListFactory)
    {
      _questRegistry = questRegistry;
      _actorRegistry = actorRegistry;
      _questStateListFactory = questStateListFactory;
    }

    public void InitializeAsNew(Guid questId, Guid initialNpcId)
    {
      CurrentState.Value = new TalkToNpcQuestState(questId, initialNpcId, _actorRegistry);
    }

    public void NextState()
    {
      CurrentState.Value = States[States.IndexOf(CurrentState.Value) + 1];
    }

    public void StartNewQuest(Guid questId)
    {
      Debug.Log($"Starting new quest {questId}");
      States = _questStateListFactory.Create(questId, _questRegistry.GetQuestConfig(questId).Description);
      CurrentState.Value = States[0];
    }

    public void CompleteCurrentQuest()
    {
      Debug.Log($"Completing quest {CurrentState.Value.QuestId}");
      States.Clear();
    }
  }
}