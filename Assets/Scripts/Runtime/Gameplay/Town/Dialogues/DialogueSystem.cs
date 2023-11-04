using System;
using System.Linq;
using RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data;
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

    public IReactiveProperty<DialogueEntryModel> CurrentDialogueEntry => _currentDialogueEntry;
    public BoolReactiveProperty IsConversationActive { get; } = new(false);
    public BoolReactiveProperty HasNextDialogueEntry { get; } = new(false);

    public Conversation CurrentConversation => _currentConversation;

    private int CurrentDialogueEntryIndex
    {
      get => _currentDialogueEntryIndex;
      set
      {
        _currentDialogueEntryIndex = value;
        HasNextDialogueEntry.Value = _currentDialogueEntryIndex < _currentConversation.Entries.Length - 1;
      }
    }

    private Conversation _currentConversation;
    private Conversation _currentConversation1;
    private int _currentDialogueEntryIndex;

    public DialogueSystem(IConversationDataBase conversationDataBase) =>
      _conversationDataBase = conversationDataBase;

    public void StartConversation(Guid conversationId)
    {
      if (_conversationDataBase.TryGetConversationById(conversationId, out _currentConversation))
      {
        Debug.Log($"Conversation {conversationId.ToString()} started");
        IsConversationActive.Value = true;
        Debug.Assert(_currentConversation.Entries.Any(), "Conversation has no entries");
        CurrentDialogueEntryIndex = 0;
        _currentDialogueEntry.Value = new DialogueEntryModel(_currentConversation.Entries[CurrentDialogueEntryIndex]);
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
      _currentDialogueEntry.Value = new DialogueEntryModel(_currentConversation.Entries[++CurrentDialogueEntryIndex]);
    }

    public void FinishConversation()
    {
      IsConversationActive.Value = false;
      _conversationDataBase.SetFinishedConversation(_currentConversation.Id);
    }
  }
}