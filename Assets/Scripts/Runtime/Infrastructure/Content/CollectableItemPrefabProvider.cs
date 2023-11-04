using Cysharp.Threading.Tasks;
using RussSurvivor.Runtime.Gameplay.Common.Items.Collectables;
using RussSurvivor.Runtime.Gameplay.Common.Quests.Data;
using UnityEngine;

namespace RussSurvivor.Runtime.Infrastructure.Content
{
  public class CollectableItemPrefabProvider : ICollectableItemPrefabProvider
  {
    private CollectablePrefabsConfig _collectablePrefabContainer;

    public async UniTask InitializeAsync()
    {
      _collectablePrefabContainer ??=
        await Resources.LoadAsync<CollectablePrefabsConfig>("Configs/CollectablePrefabsConfig") as
          CollectablePrefabsConfig;
    }

    public CollectableItem GetPrefab(CollectItemsQuestDescription.CollectableType collectingCollectableType)
    {
      return _collectablePrefabContainer.CollectablePrefabs[collectingCollectableType];
    }
  }
}