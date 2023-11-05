using System;
using System.Collections.Generic;
using RussSurvivor.Runtime.Gameplay.Common.Quests.StateMachine;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data.Conditions
{
  public class ConversationConditionSolver : IConversationConditionSolver
  {
    private readonly IQuestStateMachine _questStateMachine;
    private readonly IConversationDataBase _conversationDataBase;

    private readonly Dictionary<Type, QuestStateType> _questStateTypeByType = new()
    {
      [typeof(TalkToNpcQuestState)] = QuestStateType.TalkToNpcState,
      [typeof(CollectingQuestState)] = QuestStateType.CollectingState,
      [typeof(ReturnToTownQuestState)] = QuestStateType.ReturnToTownState,
      [typeof(GoOutsideQuestState)] = QuestStateType.GoOutsideState
    };

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
        bool questIdsAreEqualAndNonNull = QuestIdsAreEqualAndNonNull(hasQuest);
        Debug.Log($"Quest {hasQuest.QuestConfig.Id} is active: {questIdsAreEqualAndNonNull}");
        return questIdsAreEqualAndNonNull;
      }

      if (condition is OtherConversationDone otherConversationDone)
      {
        Debug.Log($"Checking if conversation {otherConversationDone.Conversation.Id} is finished");
        bool isConversationAvailable =
          _conversationDataBase.IsConversationFinished(otherConversationDone.Conversation.Id);
        Debug.Log($"Conversation {otherConversationDone.Conversation.Id} is finished: {isConversationAvailable}");
        return isConversationAvailable;
      }

      if (condition is CurrentQuestStateIs currentQuestStateIs)
      {
        Debug.Log($"Checking if quest state is {currentQuestStateIs.QuestStateType}");
        QuestStateType typeEnum = GetTypeByQuestState();
        bool equals = typeEnum == currentQuestStateIs.QuestStateType;
        Debug.Log($"Quest state is exact: {equals.ToString()}");
        return equals;
      }

      return false;
    }

    private QuestStateType GetTypeByQuestState()
    {
      Type type = _questStateMachine.CurrentState.Value.GetType();
      return _questStateTypeByType[type];
    }

    private bool QuestIdsAreEqualAndNonNull(CurrentQuestIs hasQuest)
    {
      return _questStateMachine.CurrentState.Value != null &&
             hasQuest.QuestConfig != null &&
             _questStateMachine.CurrentState.Value.QuestId == hasQuest.QuestConfig.Id;
    }
  }
}