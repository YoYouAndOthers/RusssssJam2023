using RussSurvivor.Runtime.Gameplay.Common.Quests.StateMachine;

namespace RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data.Actions
{
  public class ConversationActionInvoker : IConversationActionInvoker
  {
    private readonly IQuestStateMachine _questStateMachine;

    public ConversationActionInvoker(IQuestStateMachine questStateMachine) =>
      _questStateMachine = questStateMachine;

    public bool TryInvokeAction(DialogueActionBase action)
    {
      if (action is CompleteQuestAction completeQuest)
      {
        _questStateMachine.CompleteCurrentQuest();
        return true;
      }

      if (action is GetNewQuestAction startQuest)
      {
        _questStateMachine.StartNewQuest(startQuest.Quest.Id);
        return true;
      }

      return action is PlayActorAnimation playActorAnimation;
    }
  }
}