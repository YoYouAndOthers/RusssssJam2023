using RussSurvivor.Runtime.Gameplay.Common.Player;
using RussSurvivor.Runtime.Gameplay.Common.Quests.Resolvers;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Common.Items.Collectables
{
  public class CollectableItem : MonoBehaviour
  {
    private CollectingQuestResolver _collectingQuestResolver;

    private void OnTriggerEnter2D(Collider2D other)
    {
      if (other.attachedRigidbody.TryGetComponent(out PlayerBehaviourBase player))
      {
        _collectingQuestResolver.RemoveCollectable(this);
        Destroy(gameObject);
      }
    }

    public void Initialize(CollectingQuestResolver collectingQuestResolver)
    {
      _collectingQuestResolver = collectingQuestResolver;
    }
  }
}