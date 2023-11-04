using RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data;
using RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data.Conditions;

namespace RussSurvivor.Runtime.Gameplay.Town.Dialogues
{
  public interface IConversationConditionSolver
  {
    bool IsConversationAvailable(ConditionToStartBase condition);
  }
}