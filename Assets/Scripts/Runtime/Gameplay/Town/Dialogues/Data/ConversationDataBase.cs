using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data
{
  public class ConversationDataBase : IConversationDataBase
  {
    private readonly List<string> _conversationKeys = new() { "Dialogues", "Conversation" };
    private readonly List<string> _actorKeys = new() { "Dialogues", "Actor" };
    private readonly Dictionary<string, Conversation> _finishedConversationsById = new();
    private Dictionary<string, Actor> _actorsById = new();
    private Dictionary<string, IEnumerable<Conversation>> _actorsConversationsById = new();

    private Dictionary<string, Conversation> _conversationsById = new();

    public async UniTask InitializeAsync()
    {
      if (_conversationsById.Any() && _actorsById.Any() && _actorsConversationsById.Any())
        return;

      Debug.Log("Dialogue database initialization started");
      IList<Conversation> conversations =
        await Addressables.LoadAssetsAsync<Conversation>(_conversationKeys, null, Addressables.MergeMode.Intersection);
      IList<Actor> actors =
        await Addressables.LoadAssetsAsync<Actor>(_actorKeys, null, Addressables.MergeMode.Intersection);

      _conversationsById = conversations.ToDictionary(k => k.Id, v => v);
      _actorsById = actors.ToDictionary(k => k.Id, v => v);
      _actorsConversationsById = _actorsById
        .ToDictionary(
          k => k.Key,
          v => conversations
            .Where(c => c.Actors
              .Select(k => k.Id)
              .Contains(v.Key)));
      Debug.Log("Dialogue database initialization finished");
    }

    public bool TryGetConversationById(string id, out Conversation conversation)
    {
      return _conversationsById.TryGetValue(id, out conversation);
    }

    public bool IsConversationFinished(string conversationId)
    {
      return _finishedConversationsById.ContainsKey(conversationId);
    }

    public bool GetActorById(string id, out Actor actor)
    {
      return _actorsById.TryGetValue(id, out actor);
    }

    public bool GetActorsConversations(string actorId, out IEnumerable<Conversation> conversations)
    {
      if (_actorsConversationsById.TryGetValue(actorId, out IEnumerable<Conversation> allConversations))
      {
        conversations = allConversations.Where(k => !_finishedConversationsById.ContainsKey(k.Id));
        return true;
      }

      conversations = null;
      return false;
    }

    public void SetFinishedConversation(string conversationId)
    {
      Debug.Assert(_conversationsById.ContainsKey(conversationId), "_conversationsById.ContainsKey(conversationId)");
      if (!_conversationsById[conversationId].IsRepeatable)
      {
        Debug.Log($"Conversation {conversationId.ToString()} marked as finished");
        _finishedConversationsById.TryAdd(conversationId, _conversationsById[conversationId]);
      }
    }
  }
}