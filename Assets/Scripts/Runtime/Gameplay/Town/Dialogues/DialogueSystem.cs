using System;
using System.Linq;
using RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data;
using RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data.Actions;
using RussSurvivor.Runtime.Gameplay.Town.Dialogues.Models;
using UniRx;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Town.Dialogues
{
  public class DialogueSystem : IDialogueSystem
  {
    private readonly ReactiveProperty<DialogueEntryModel> _currentDialogueEntry =
      new() { Value = DialogueEntryModel.Empty };

    private readonly IConversationDataBase _conversationDataBase;
    private readonly IConversationActionInvoker _actionInvoker;

    public IReactiveProperty<DialogueEntryModel> CurrentDialogueEntry => _currentDialogueEntry;
    public BoolReactiveProperty IsConversationActive { get; } = new(false);
    public BoolReactiveProperty HasNextDialogueEntry { get; } = new(false);

    public Conversation CurrentConversation => _currentConversation;

    private int CurrentDialogueEntryIndex
    {
      get => _currentDialogueEntryIndex;
      set
      {
        DialogueEntry currentConversationEntry = _currentConversation.Entries[value];
        _currentDialogueEntryIndex = value;
        foreach (DialogueActionBase action in currentConversationEntry.Actions)
          if (!_actionInvoker.TryInvokeAction(action))
            Debug.LogError($"Action {action.GetType().Name} not invoked!");
        _currentDialogueEntry.Value = new DialogueEntryModel(currentConversationEntry);
        HasNextDialogueEntry.Value = _currentDialogueEntryIndex < _currentConversation.Entries.Length - 1;
      }
    }

    private Conversation _currentConversation;
    private Conversation _currentConversation1;
    private int _currentDialogueEntryIndex;

    public DialogueSystem(IConversationDataBase conversationDataBase, IConversationActionInvoker actionInvoker)
    {
      _conversationDataBase = conversationDataBase;
      _actionInvoker = actionInvoker;
    }

    public void StartConversation(Guid conversationId)
    {
      if (_conversationDataBase.TryGetConversationById(conversationId, out _currentConversation))
      {
        Debug.Log($"Conversation {conversationId.ToString()} started");
        IsConversationActive.Value = true;
        Debug.Assert(_currentConversation.Entries.Any(), "Conversation has no entries");
        CurrentDialogueEntryIndex = 0;
      }
      else
      {
        Debug.LogError($"Conversation with id {conversationId.ToString()} not found");
        IsConversationActive.Value = false;
      }
    }

    public void NextPhrase()
    {
      Debug.Assert(_currentConversation != null, "No conversation is active");
      Debug.Assert(_currentDialogueEntryIndex < _currentConversation.Entries.Length - 1,
        "No more entries in conversation");
      CurrentDialogueEntryIndex++;
    }

    public void CancelConversation()
    {
      IsConversationActive.Value = false;
      Debug.Log($"Conversation {_currentConversation.Id.ToString()} canceled");
    }

    public void FinishConversation()
    {
      IsConversationActive.Value = false;
      if (_currentConversation.OnEndActions != null)
        foreach (DialogueActionBase onEndAction in _currentConversation.OnEndActions)
          if (!_actionInvoker.TryInvokeAction(onEndAction))
            Debug.LogError($"Action {onEndAction.GetType().Name} not invoked!");

      _conversationDataBase.SetFinishedConversation(_currentConversation.Id);
      Debug.Log($"Conversation {_currentConversation.Id.ToString()} finished");
    }
  }
}