using System;

namespace RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data.Conditions
{
  [Serializable]
  public class CurrentQuestStateIs : ConditionToStartBase
  {
    public string name = "Quest State Is";
    public QuestStateType QuestStateType;
  }
}