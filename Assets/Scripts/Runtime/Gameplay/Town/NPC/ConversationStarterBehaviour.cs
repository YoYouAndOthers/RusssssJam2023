using System.Collections.Generic;
using System.Linq;
using RussSurvivor.Runtime.Gameplay.Common.Player;
using RussSurvivor.Runtime.Gameplay.Town.Characters;
using RussSurvivor.Runtime.Gameplay.Town.Dialogues;
using RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data;
using UnityEngine;
using Zenject;

namespace RussSurvivor.Runtime.Gameplay.Town.NPC
{
  public class ConversationStarterBehaviour : IntarectableNpcBehaviourBase
  {
    [SerializeField] private Actor _actor;

    private IEnumerable<Conversation> Conversations =>
      _conversationDataBase.GetActorsConversations(_actor.Id, out IEnumerable<Conversation> conversations)
        ? conversations
        : null;

    private IActorRegistry _actorRegistry;

    private IConversationDataBase _conversationDataBase;
    private IDialogueSystem _dialogueSystem;

    [Inject]
    private void Construct(IConversationDataBase conversationDataBase, IDialogueSystem dialogueSystem,
      IActorRegistry actorRegistry)
    {
      _conversationDataBase = conversationDataBase;
      _dialogueSystem = dialogueSystem;
      _actorRegistry = actorRegistry;
    }

    private void Awake()
    {
      _actorRegistry.RegisterActor(this, _actor.Id);
    }

    protected override void PerformInteraction(PlayerTownBehaviour player)
    {
      Debug.Log("Starting conversation");
      Debug.Assert(Conversations != null, "Conversations != null");
      if (Conversations.Any(k => k.ConditionsToStart.All(l => l.IsMet())))
      {
        Conversation conversation = Conversations.First(k => k.ConditionsToStart.All(l => l.IsMet()));
        _dialogueSystem.StartConversation(conversation.Id);
      }
      else
      {
        Debug.Log("No conversation to start");
      }
    }
  }
}