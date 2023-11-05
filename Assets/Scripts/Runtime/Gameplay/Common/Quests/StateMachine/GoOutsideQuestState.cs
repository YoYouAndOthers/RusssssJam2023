using System;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Common.Quests.StateMachine
{
  public class GoOutsideQuestState : QuestWithDirectionState
  {
    public GoOutsideQuestState(Guid questId) : base(questId)
    {
    }

    public override Vector2 GetPosition()
    {
      return Vector2.zero;
    }
  }
}