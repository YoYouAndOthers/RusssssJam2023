using System;

namespace RussSurvivor.Runtime.Gameplay.Common.Quests.Data
{
  [Serializable]
  public class DefenceQuestDescription : QuestDescriptionBase
  {
    public enum ObjectToDefend
    {
      None = 0,
      Idol = 1,
      Fire = 2
    }

    public ObjectToDefend Object;
    public float TimeToDefendSeconds;
  }
}