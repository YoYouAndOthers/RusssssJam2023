using Cysharp.Threading.Tasks;
using RussSurvivor.Runtime.Gameplay.Common.Items.Collectables;
using RussSurvivor.Runtime.Gameplay.Common.Quests.Data;

namespace RussSurvivor.Runtime.Infrastructure.Content
{
  public interface ICollectableItemPrefabProvider
  {
    CollectableItem GetPrefab(CollectItemsQuestDescription.CollectableType collectingCollectableType);
    UniTask InitializeAsync();
  }
}