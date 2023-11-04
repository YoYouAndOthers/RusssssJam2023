using System;
using RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data;

namespace RussSurvivor.Runtime.Gameplay.Common.Quests.Data
{
  [Serializable]
  public abstract class QuestDescriptionBase
  {
    public Actor QuestRewardGiver;
  }
}