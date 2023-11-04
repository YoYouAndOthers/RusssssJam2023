using System;

namespace RussSurvivor.Runtime.Gameplay.Common.Quests.StateMachine
{
  public interface IQuestStateMachine
  {
    QuestState CurrentState { get; }
    void InitializeAsNew(Guid questId, Guid initialNpcId);
  }
}