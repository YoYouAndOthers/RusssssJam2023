using System;
using RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data;

namespace RussSurvivor.Runtime.Gameplay.Common.Quests.Data
{
  [Serializable]
  public class ReturnItemQuestDescription : QuestDescriptionBase
  {
    public enum ItemToReturnType
    {
      None = 0,
      GoldenJug = 1,
      FamilyPortrait = 2,
      AncestralIdol = 3,
      Necklace = 4,
    }

    public ItemToReturnType ItemToReturn;
    public Actor BringTo;
  }
}