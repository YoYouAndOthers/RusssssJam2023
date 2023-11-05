using System;
using RussSurvivor.Runtime.Gameplay.Common.Quests.Data;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Common.Quests.StateMachine
{
  public class DestroyNpcQuestState : QuestWithDirectionState
  {
    public DestructionQuestDescription.HostileObjectType DestructionQuestHostileType;
    
    public DestroyNpcQuestState(Guid questId,
      DestructionQuestDescription.HostileObjectType destructionQuestHostileObject) : base(questId) =>
      DestructionQuestHostileType = destructionQuestHostileObject;

    public override Vector2 GetPosition()
    {
      return default;
    }
  }
}