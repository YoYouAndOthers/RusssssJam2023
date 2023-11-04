using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using RussSurvivor.Runtime.Gameplay.Common.Player;
using RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Town.NPC
{
  public class ConversationStarterBehaviour : IntarectableNpcBehaviourBase
  {
    [SerializeField] private Conversation _conversation;
    [SerializeField] private Actor _actor;

    protected override async void PerformInteraction(PlayerTownBehaviour player)
    {
      Debug.Log("Starting conversation");
      for (var i = 0; i < _conversation.Entries.Length; i++)
      {
        DialogueEntry entry = _conversation.Entries[i];
        await UniTask.Delay(TimeSpan.FromSeconds(1));
        Debug.Log($"{entry.Speaker.Name}: {entry.Text}");
        if(entry.Actions.OfType<EndConversationAction>().Any())
          Debug.Log("Conversation ended");
      }
    }
  }
}