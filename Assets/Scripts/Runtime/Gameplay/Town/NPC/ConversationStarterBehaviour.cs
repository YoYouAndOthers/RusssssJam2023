using System;
using System.Collections.Generic;
using System.Linq;
using RussSurvivor.Runtime.Gameplay.Common.Player;
using RussSurvivor.Runtime.Gameplay.Common.Timing;
using RussSurvivor.Runtime.Gameplay.Town.Characters;
using RussSurvivor.Runtime.Gameplay.Town.Dialogues;
using RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data;
using RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data.Conditions;
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
    private IConversationConditionSolver _conversationConditionSolver;
    private IConversationDataBase _conversationDataBase;
    private IPauseService _pauseService;
    private IDialogueSystem _dialogueSystem;

    [Inject]
    private void Construct(
      IConversationDataBase conversationDataBase,
      IConversationConditionSolver conversationConditionSolver,
      IDialogueSystem dialogueSystem,
      IPauseService pauseService,
      IActorRegistry actorRegistry)
    {
      _conversationDataBase = conversationDataBase;
      _conversationConditionSolver = conversationConditionSolver;
      _dialogueSystem = dialogueSystem;
      _pauseService = pauseService;
      _actorRegistry = actorRegistry;
    }

    private void Awake()
    {
      _actorRegistry.RegisterActor(this, _actor.Id);
    }

    private void OnDestroy()
    {
      _actorRegistry.CleanActor(_actor.Id);
    }

    protected override void PerformInteraction(PlayerTownBehaviour player)
    {
      Debug.Log("Starting conversation");
      Debug.Assert(Conversations != null, "Conversations != null");
      Func<ConditionToStartBase, bool>
        conditionPredicate = l => _conversationConditionSolver.IsConversationAvailable(l);
      if (Conversations.Any(k => k.ConditionsToStart.All(conditionPredicate)))
      {
        Conversation conversation = Conversations.First(k => k.ConditionsToStart.All(conditionPredicate));
        _pauseService.Pause();
        _dialogueSystem.StartConversation(conversation.Id);
      }
      else
      {
        Debug.Log("No conversation to start");
      }
    }

    protected override void PerformInteractionExit(PlayerTownBehaviour player)
    {
      Debug.Log("Leaving conversation");
      if(_dialogueSystem.HasNextDialogueEntry.Value)
        _dialogueSystem.CancelConversation();
      else
        _dialogueSystem.FinishConversation();
    }
  }
}