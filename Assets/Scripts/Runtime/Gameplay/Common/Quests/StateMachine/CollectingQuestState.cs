using System;
using RussSurvivor.Runtime.Gameplay.Common.Quests.Data;

namespace RussSurvivor.Runtime.Gameplay.Common.Quests.StateMachine
{
  public class CollectingQuestState : QuestState
  {
    public CollectingQuestState(Guid questId, CollectItemsQuestDescription collectionDescription) : base(questId)
    {
    }
  }
}