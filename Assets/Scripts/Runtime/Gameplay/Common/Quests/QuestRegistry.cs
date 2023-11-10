using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using RussSurvivor.Runtime.Gameplay.Common.Quests.Data;
using UnityEngine.AddressableAssets;

namespace RussSurvivor.Runtime.Gameplay.Common.Quests
{
  public class QuestRegistry : IQuestRegistry
  {
    private Dictionary<string, QuestConfig> _questConfigs = new();

    public async UniTask InitializeAsync()
    {
      IList<QuestConfig> allQuests = await Addressables.LoadAssetsAsync<QuestConfig>(new List<string> { "Quests" },
        null, Addressables.MergeMode.Intersection);
      _questConfigs = allQuests.ToDictionary(x => x.Id, x => x);
    }

    public QuestConfig GetQuestConfig(string id)
    {
      return _questConfigs[id];
    }
  }
}