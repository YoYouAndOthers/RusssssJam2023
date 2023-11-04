using System;
using UniRx;

namespace RussSurvivor.Runtime.Gameplay.Common.Quests.StateMachine
{
  public interface IQuestStateMachine
  {
    IReactiveProperty<QuestState> CurrentState { get; }
    void InitializeAsNew(Guid questId, Guid initialNpcId);
    void StartNewQuest(Guid questId);
    void CompleteCurrentQuest();
    void NextState();
  }
}