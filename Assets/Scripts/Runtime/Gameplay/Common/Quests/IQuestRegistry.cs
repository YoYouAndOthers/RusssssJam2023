using System;
using Cysharp.Threading.Tasks;
using RussSurvivor.Runtime.Gameplay.Common.Quests.Data;

namespace RussSurvivor.Runtime.Gameplay.Common.Quests
{
  public interface IQuestRegistry
  {
    UniTask InitializeAsync();
    QuestConfig GetQuestConfig(Guid id);
  }
}