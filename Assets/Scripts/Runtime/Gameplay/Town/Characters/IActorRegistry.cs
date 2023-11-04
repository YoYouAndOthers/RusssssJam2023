using System;
using RussSurvivor.Runtime.Gameplay.Town.NPC;

namespace RussSurvivor.Runtime.Gameplay.Town.Characters
{
  public interface IActorRegistry
  {
    bool TryGetActor(Guid id, out ConversationStarterBehaviour actor);
    void RegisterActor(ConversationStarterBehaviour actor, Guid id);
  }
}