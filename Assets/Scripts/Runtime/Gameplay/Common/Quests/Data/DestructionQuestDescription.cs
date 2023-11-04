using System;

namespace RussSurvivor.Runtime.Gameplay.Common.Quests.Data
{
  [Serializable]
  public class DestructionQuestDescription : QuestDescriptionBase
  {
    public enum HostileObjectType
    {
      None = 0,
      SwampMaker = 1,
      WaterSpoiler = 2,
      Lizapult = 3,
      BombStorage = 4,
      LizardCamp = 5,
      LizardIdol = 6,
    }

    public HostileObjectType HostileObject;
  }
}