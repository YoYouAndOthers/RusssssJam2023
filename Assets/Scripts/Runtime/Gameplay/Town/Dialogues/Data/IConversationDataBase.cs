using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data
{
  public interface IConversationDataBase
  {
    UniTask InitializeAsync();
    bool TryGetConversationById(string id, out Conversation conversation);
    bool IsConversationFinished(string conversationId);
    bool GetActorById(string id, out Actor actor);
    bool GetActorsConversations(string actorId, out IEnumerable<Conversation> conversations);
    void SetFinishedConversation(string conversationId);
  }
}