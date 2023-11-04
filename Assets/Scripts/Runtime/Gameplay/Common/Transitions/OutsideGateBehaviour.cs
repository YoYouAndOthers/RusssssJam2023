using RussSurvivor.Runtime.Gameplay.Common.Player;
using UnityEngine;
using Zenject;

namespace RussSurvivor.Runtime.Gameplay.Common.Transitions
{
  [RequireComponent(typeof(Collider2D))]
  public class OutsideGateBehaviour : MonoBehaviour
  {
    private IGameplayTransitionService _transitionService;

    [Inject]
    private void Construct(IGameplayTransitionService transitionService)
    {
      _transitionService = transitionService;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      if (other.TryGetComponent(out PlayerTownBehaviour _))
        _transitionService.GoToBattle();
    }
  }
}