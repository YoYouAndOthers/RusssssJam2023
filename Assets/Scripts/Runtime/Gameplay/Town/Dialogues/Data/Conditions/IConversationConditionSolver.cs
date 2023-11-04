namespace RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data.Conditions
{
  public interface IConversationConditionSolver
  {
    bool IsConversationAvailable(ConditionToStartBase condition);
  }
}