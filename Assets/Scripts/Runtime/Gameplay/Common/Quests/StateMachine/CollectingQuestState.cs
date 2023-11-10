using RussSurvivor.Runtime.Gameplay.Common.Quests.Data;

namespace RussSurvivor.Runtime.Gameplay.Common.Quests.StateMachine
{
  public class CollectingQuestState : QuestState
  {
    public readonly CollectItemsQuestDescription.CollectableType CollectableType;
    public readonly int CollectablesCount;
    public int CollectedAmount;

    public CollectingQuestState(string questId, CollectItemsQuestDescription collectionDescription) : base(questId)
    {
      CollectableType = collectionDescription.CollectablesType;
      CollectablesCount = collectionDescription.CollectablesCount;
      CollectedAmount = 0;
    }
  }
}