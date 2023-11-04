using System;

namespace RussSurvivor.Runtime.Gameplay.Common.Quests.StateMachine
{
  public abstract class QuestState
  {
    public Guid QuestId { get; }

    protected QuestState(Guid questId) =>
      QuestId = questId;
  }
}