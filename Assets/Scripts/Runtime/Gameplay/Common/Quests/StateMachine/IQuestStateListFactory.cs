using System.Collections.Generic;
using RussSurvivor.Runtime.Gameplay.Common.Quests.Data;

namespace RussSurvivor.Runtime.Gameplay.Common.Quests.StateMachine
{
  public interface IQuestStateListFactory
  {
    List<QuestState> Create(string questId, QuestDescriptionBase description);
  }
}