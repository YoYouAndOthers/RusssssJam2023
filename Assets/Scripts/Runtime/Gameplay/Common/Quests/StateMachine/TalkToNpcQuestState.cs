using System;
using RussSurvivor.Runtime.Gameplay.Town.Characters;
using RussSurvivor.Runtime.Gameplay.Town.NPC;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Common.Quests.StateMachine
{
  public class TalkToNpcQuestState : QuestState
  {
    private readonly Guid _npcId;
    private readonly IActorRegistry _actorRegistry;

    public TalkToNpcQuestState(Guid id, IActorRegistry actorRegistry)
    {
      _npcId = id;
      _actorRegistry = actorRegistry;
    }

    public override Vector2 GetPosition()
    {
      bool actorExists = _actorRegistry.TryGetActor(_npcId, out ConversationStarterBehaviour actor);
      if (actorExists)
        return actor.transform.position;

      throw new Exception($"Actor with id {_npcId} does not exist in the registry.");
    }
  }
}