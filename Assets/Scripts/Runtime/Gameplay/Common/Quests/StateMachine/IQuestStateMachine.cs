using System;

namespace RussSurvivor.Runtime.Gameplay.Common.Quests.StateMachine
{
  public interface IQuestStateMachine
  {
    void InitializeAsNew(Guid initialNpcId);
    QuestState CurrentState { get; }
  }
}