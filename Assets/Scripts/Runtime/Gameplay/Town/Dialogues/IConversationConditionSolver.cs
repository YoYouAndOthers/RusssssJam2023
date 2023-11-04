using RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data;

namespace RussSurvivor.Runtime.Gameplay.Town.Dialogues
{
  public interface IConversationConditionSolver
  {
    bool IsConversationAvailable(ConditionToStartBase condition);
  }
}