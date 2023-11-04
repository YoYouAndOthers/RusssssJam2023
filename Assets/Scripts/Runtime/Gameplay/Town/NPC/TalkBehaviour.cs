using RussSurvivor.Runtime.Gameplay.Common.Player;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Town.NPC
{
  public class TalkBehaviour : MonoBehaviour
  {
    private void OnTriggerEnter2D(Collider2D other)
    {
      if (other.attachedRigidbody.TryGetComponent(out PlayerBehaviourBase player))
      {
        Debug.Log("Talk to NPC");
      }
    }
  }
}