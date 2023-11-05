using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Common.Quests.StateMachine
{
  public class QuestStateMachine : IQuestStateMachine
  {
    private readonly IQuestRegistry _questRegistry;
    private readonly IQuestStateListFactory _questStateListFactory;
    public IReactiveProperty<QuestState> CurrentState { get; } = new ReactiveProperty<QuestState>();
    private List<QuestState> States { get; set; } = new();

    public QuestStateMachine(IQuestRegistry questRegistry, IQuestStateListFactory questStateListFactory)
    {
      _questRegistry = questRegistry;
      _questStateListFactory = questStateListFactory;
    }

    public void NextState<T>() where T : QuestState
    {
      CurrentState.Value = States.First(k => k.GetType() == typeof(T));
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