using System;
using RussSurvivor.Runtime.Gameplay.Common.Quests.Data;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Common.Quests.StateMachine
{
  public class DestroyNpcQuestState : QuestWithDirectionState
  {
    public readonly DestructionQuestDescription.HostileObjectType DestructionQuestHostileType;
    public readonly int NpcToDestroyCount;
    private readonly Vector2 _position;

    public DestroyNpcQuestState(Guid questId,
      DestructionQuestDescription description) : base(questId)
    {
      DestructionQuestHostileType = description.HostileObject;
      NpcToDestroyCount = description.AmountToDestroy;
      _position = description.Position;
    }

    public override Vector2 GetPosition()
    {
      Debug.Log($"DestroyNpcQuestState: {_position}");
      return _position;
    }
  }
}