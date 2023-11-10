namespace RussSurvivor.Runtime.Gameplay.Common.Quests.StateMachine
{
  public abstract class QuestState
  {
    public string QuestId { get; }

    protected QuestState(string questId) =>
      QuestId = questId;
  }
}