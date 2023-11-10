using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Common.Quests.StateMachine
{
  public abstract class QuestWithDirectionState : QuestState
  {
    protected QuestWithDirectionState(string questId) : base(questId)
    {
    }

    public abstract Vector2 GetPosition();
  }
}