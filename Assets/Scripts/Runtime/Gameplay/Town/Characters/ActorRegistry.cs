using System;
using System.Collections.Generic;
using RussSurvivor.Runtime.Gameplay.Town.NPC;

namespace RussSurvivor.Runtime.Gameplay.Town.Characters
{
  public class ActorRegistry : IActorRegistry
  {
    private readonly Dictionary<Guid, IntarectableNpcBehaviourBase> _actors = new();

    public bool TryGetActor(Guid id, out IntarectableNpcBehaviourBase actor)
    {
      return _actors.TryGetValue(id, out actor);
    }

    public void RegisterActor(IntarectableNpcBehaviourBase actor, Guid id)
    {
      if (!_actors.TryAdd(id, actor))
        _actors[id] = actor;
    }

    public void CleanActor(Guid actorId)
    {
      _actors[actorId] = null;
    }
  }
}