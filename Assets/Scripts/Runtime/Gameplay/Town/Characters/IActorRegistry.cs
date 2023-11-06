using System;
using RussSurvivor.Runtime.Gameplay.Town.NPC;

namespace RussSurvivor.Runtime.Gameplay.Town.Characters
{
  public interface IActorRegistry
  {
    bool TryGetActor(Guid id, out IntarectableNpcBehaviourBase actor);
    void RegisterActor(IntarectableNpcBehaviourBase actor, Guid id);
    void CleanActor(Guid actorId);
  }
}