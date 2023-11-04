using System;
using RussSurvivor.Runtime.Gameplay.Common.Quests.Data;

namespace RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data.Actions
{
  [Serializable]
  public class GetNewQuestAction : DialogueActionBase
  {
    public string name;
    public QuestConfig Quest;
  }
}