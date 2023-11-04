using System;
using RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data;
using RussSurvivor.Runtime.Gameplay.Town.Dialogues.Models;
using UniRx;

namespace RussSurvivor.Runtime.Gameplay.Town.Dialogues
{
  public interface IDialogueSystem
  {
    IReactiveProperty<DialogueEntryModel> CurrentDialogueEntry { get; }
    BoolReactiveProperty IsConversationActive { get; }
    BoolReactiveProperty HasNextDialogueEntry { get; }
    Conversation CurrentConversation { get; }
    void StartConversation(Guid conversationId);
    void NextPhrase();
    void CancelConversation();
    void FinishConversation();
  }
}