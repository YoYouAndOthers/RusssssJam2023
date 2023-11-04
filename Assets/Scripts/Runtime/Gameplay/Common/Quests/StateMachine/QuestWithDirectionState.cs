using System;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Common.Quests.StateMachine
{
  public abstract class QuestWithDirectionState : QuestState
  {
    protected QuestWithDirectionState(Guid questId) : base(questId)
    {
    }

    public abstract Vector2 GetPosition();
  }
}