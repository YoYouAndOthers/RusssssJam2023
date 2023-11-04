using RussSurvivor.Runtime.Gameplay.Common.Quests.Data;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Common.Quests.StateMachine
{
  public class CollectingQuestState : QuestState
  {
    public CollectingQuestState(CollectItemsQuestDescription collectionDescription)
    {
    }

    public override Vector2 GetPosition()
    {
      return default;
    }
  }
}