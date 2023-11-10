using System.Collections.Generic;
using RussSurvivor.Runtime.Gameplay.Town.NPC;

namespace RussSurvivor.Runtime.Gameplay.Town.Characters
{
  public class ActorRegistry : IActorRegistry
  {
    private readonly Dictionary<string, IntarectableNpcBehaviourBase> _actors = new();

    public bool TryGetActor(string id, out IntarectableNpcBehaviourBase actor)
    {
      return _actors.TryGetValue(id, out actor);
    }

    public void RegisterActor(IntarectableNpcBehaviourBase actor, string id)
    {
      if (!_actors.TryAdd(id, actor))
        _actors[id] = actor;
    }

    public void CleanActor(string actorId)
    {
      _actors[actorId] = null;
    }
  }
}