using AYellowpaper.SerializedCollections;
using RussSurvivor.Runtime.Gameplay.Common.Quests.Data;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Common.Items.Collectables
{
  [CreateAssetMenu(fileName = "CollectablePrefabsConfig", menuName = "RussSurvivor/CollectablePrefabsConfig")]
  public class CollectablePrefabsConfig : ScriptableObject
  {
    public SerializedDictionary<CollectItemsQuestDescription.CollectableType, CollectableItem> CollectablePrefabs;
  }
}