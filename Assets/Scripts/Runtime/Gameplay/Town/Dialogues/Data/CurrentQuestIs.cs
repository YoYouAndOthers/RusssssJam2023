using System;
using RussSurvivor.Runtime.Gameplay.Common.Quests.Data;

namespace RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data
{
  [Serializable]
  public sealed class CurrentQuestIs : ConditionToStartBase
  {
    public string name = "Has Quest";
    public QuestConfig QuestConfig;
  }
}