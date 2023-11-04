namespace RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data.Actions
{
  public interface IConversationActionInvoker
  {
    bool TryInvokeAction(DialogueActionBase action);
  }
}