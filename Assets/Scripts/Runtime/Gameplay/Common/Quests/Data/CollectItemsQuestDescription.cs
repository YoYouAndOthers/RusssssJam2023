using System;
using RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data;

namespace RussSurvivor.Runtime.Gameplay.Common.Quests.Data
{
  [Serializable]
  public class CollectItemsQuestDescription : QuestDescriptionBase
  {
    public enum CollectableType
    {
      None = 0,
      Berries = 1,
      Mushrooms = 2,
      SlavicVedas = 3,
    }

    public CollectableType CollectablesType;
    public int CollectablesCount;
    public Actor BringTo;
  }
}