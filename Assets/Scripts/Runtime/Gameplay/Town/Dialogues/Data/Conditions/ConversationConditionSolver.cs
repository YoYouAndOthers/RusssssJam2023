using RussSurvivor.Runtime.Gameplay.Common.Quests.StateMachine;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data.Conditions
{
  public class ConversationConditionSolver : IConversationConditionSolver
  {
    private readonly IQuestStateMachine _questStateMachine;
    private readonly IConversationDataBase _conversationDataBase;

    public ConversationConditionSolver(IQuestStateMachine questStateMachine, IConversationDataBase conversationDataBase)
    {
      _questStateMachine = questStateMachine;
      _conversationDataBase = conversationDataBase;
    }

    public bool IsConversationAvailable(ConditionToStartBase condition)
    {
      if (condition is CurrentQuestIs hasQuest)
      {
        Debug.Log($"Checking if quest {hasQuest.QuestConfig.Id} is active");
        return _questStateMachine.CurrentState.Value != null &&
               hasQuest.QuestConfig != null &&
               _questStateMachine.CurrentState.Value.QuestId == hasQuest.QuestConfig.Id;
      }

      if (condition is OtherConversationDone otherConversationDone)
      {
        Debug.Log($"Checking if conversation {otherConversationDone.Conversation.Id} is finished");
        return _conversationDataBase.IsConversationFinished(otherConversationDone.Conversation.Id);
      }

      return false;
    }
  }
}