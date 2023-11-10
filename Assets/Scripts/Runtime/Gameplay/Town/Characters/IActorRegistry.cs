using RussSurvivor.Runtime.Gameplay.Town.NPC;

namespace RussSurvivor.Runtime.Gameplay.Town.Characters
{
  public interface IActorRegistry
  {
    bool TryGetActor(string id, out IntarectableNpcBehaviourBase actor);
    void RegisterActor(IntarectableNpcBehaviourBase actor, string id);
    void CleanActor(string actorId);
  }
}