using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using RussSurvivor.Runtime.Gameplay.Common.Player;
using RussSurvivor.Runtime.Gameplay.Town.Dialogues;
using RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data;
using UniRx;
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
    private IDialogueSystem _dialogueSystem;

    [Inject]
    private void Construct(IConversationDataBase conversationDataBase, IDialogueSystem dialogueSystem)
    {
      _conversationDataBase = conversationDataBase;
      _dialogueSystem = dialogueSystem;
    }

    private void Awake()
    {
      _dialogueSystem.CurrentDialogueEntry.ObserveEveryValueChanged(k => k.Value)
        .Subscribe(k => Debug.Log($"{k.ActorName}: {k.Text}"));
    }

    protected override async void PerformInteraction(PlayerTownBehaviour player)
    {
      Debug.Log("Starting conversation");
      Debug.Assert(Conversations != null, "Conversations != null");
      if (Conversations.Any(k => k.ConditionsToStart.All(l => l.IsMet())))
      {
        Conversation conversation = Conversations.First(k => k.ConditionsToStart.All(l => l.IsMet()));
        _dialogueSystem.StartConversation(conversation.Id);
        while (_dialogueSystem.HasNextDialogueEntry.Value)
        {
          await UniTask.Delay(TimeSpan.FromSeconds(1));
          _dialogueSystem.NextPhrase();
        }

        await UniTask.Delay(TimeSpan.FromSeconds(1));
        _dialogueSystem.FinishConversation();
        Debug.Log($"Conversation {conversation.Id.ToString()} finished");
      }
      else
      {
        Debug.Log("No conversation to start");
      }
    }
  }
}