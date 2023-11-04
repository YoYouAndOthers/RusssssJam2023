using System;
using System.Collections.Generic;
using RussSurvivor.Runtime.Gameplay.Town.NPC;

namespace RussSurvivor.Runtime.Gameplay.Town.Characters
{
  public class ActorRegistry : IActorRegistry
  {
    private readonly Dictionary<Guid, ConversationStarterBehaviour> _actors = new();

    public bool TryGetActor(Guid id, out ConversationStarterBehaviour actor)
    {
      return _actors.TryGetValue(id, out actor);
    }

    public void RegisterActor(ConversationStarterBehaviour actor, Guid id)
    {
      if (!_actors.TryAdd(id, actor))
        _actors[id] = actor;
    }
  }
}