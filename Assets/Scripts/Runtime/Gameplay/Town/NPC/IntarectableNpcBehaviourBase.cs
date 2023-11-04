using System;
using Cysharp.Threading.Tasks;
using RussSurvivor.Runtime.Gameplay.Common.Player;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Town.NPC
{
  [RequireComponent(typeof(Collider2D))]
  public abstract class IntarectableNpcBehaviourBase : MonoBehaviour
  {
    private async void OnTriggerEnter2D(Collider2D other)
    {
      if (other.attachedRigidbody.TryGetComponent(out PlayerTownBehaviour player))
      {
        await UniTask.Delay(TimeSpan.FromSeconds(1));
        Debug.Log("Player entered the trigger");
        PerformInteraction(player);
      }
    }

    protected abstract void PerformInteraction(PlayerTownBehaviour player);
  }
}