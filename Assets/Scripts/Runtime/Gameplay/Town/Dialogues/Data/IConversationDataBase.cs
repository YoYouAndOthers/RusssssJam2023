using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data
{
  public interface IConversationDataBase
  {
    UniTask InitializeAsync();
    bool TryGetConversationById(Guid id, out Conversation conversation);
    bool IsConversationFinished(Guid conversationId);
    bool GetActorById(Guid id, out Actor actor);
    bool GetActorsConversations(Guid actorId, out IEnumerable<Conversation> conversations);
    void SetFinishedConversation(Guid conversationId);
  }
}