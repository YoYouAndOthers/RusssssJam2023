using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using RussSurvivor.Runtime.Gameplay.Common.Player;
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

    private IConversationDataBase _conversationDataBase;

    [Inject]
    private void Construct(IConversationDataBase conversationDataBase)
    {
      _conversationDataBase = conversationDataBase;
    }

    protected override async void PerformInteraction(PlayerTownBehaviour player)
    {
      Debug.Log("Starting conversation");
      Debug.Assert(Conversations != null, "Conversations != null");
      if (Conversations.Any(k => k.ConditionsToStart.All(l => l.IsMet())))
      {
        Conversation conversation = Conversations.First(k => k.ConditionsToStart.All(l => l.IsMet()));
        Debug.Log($"Conversation {conversation.Id} started");
        foreach (DialogueEntry dialogueEntry in conversation.Entries)
        {
          await UniTask.Delay(TimeSpan.FromSeconds(2));
          Debug.Log(dialogueEntry.Speaker.Name + ": " + dialogueEntry.Text);
        }

        _conversationDataBase.SetFinishedConversation(conversation.Id);
        Debug.Log($"Conversation {conversation.Id} finished");
      }
      else
      {
        Debug.Log("No conversation to start");
      }
    }
  }
}