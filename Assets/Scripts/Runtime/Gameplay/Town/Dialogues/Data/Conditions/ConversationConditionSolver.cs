using RussSurvivor.Runtime.Gameplay.Common.Quests.StateMachine;

namespace RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data.Conditions
{
  public class ConversationConditionSolver : IConversationConditionSolver
  {
    private readonly IQuestStateMachine _questStateMachine;

    public ConversationConditionSolver(IQuestStateMachine questStateMachine) =>
      _questStateMachine = questStateMachine;

    public bool IsConversationAvailable(ConditionToStartBase condition)
    {
      if (condition is CurrentQuestIs hasQuest)
      {
        return _questStateMachine.CurrentState.Value.QuestId == hasQuest.QuestConfig.Id;
      }
      return false;
    }
  }
}