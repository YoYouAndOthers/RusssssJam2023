using System;
using RussSurvivor.Runtime.Gameplay.Town.Characters;
using RussSurvivor.Runtime.Gameplay.Town.NPC;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Common.Quests.StateMachine
{
  public class TalkToNpcQuestState : QuestWithDirectionState
  {
    private readonly Guid _npcId;
    private readonly IActorRegistry _actorRegistry;

    public TalkToNpcQuestState(Guid questId, Guid npcId, IActorRegistry actorRegistry) : base(questId)
    {
      _npcId = npcId;
      _actorRegistry = actorRegistry;
    }

    public override Vector2 GetPosition()
    {
      bool actorExists = _actorRegistry.TryGetActor(_npcId, out IntarectableNpcBehaviourBase actor);
      if (actorExists)
        return actor.transform.position;

      throw new Exception($"Actor with id {_npcId} does not exist in the registry.");
    }
  }
}