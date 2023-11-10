using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Common.Quests.StateMachine
{
  public class ReturnToTownQuestState : QuestWithDirectionState
  {
    public ReturnToTownQuestState(string questId) : base(questId)
    {
    }

    public override Vector2 GetPosition()
    {
      return Vector2.zero;
    }
  }
}