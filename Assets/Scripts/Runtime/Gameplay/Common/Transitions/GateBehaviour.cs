using RussSurvivor.Runtime.Gameplay.Battle.Enemies;
using RussSurvivor.Runtime.Gameplay.Battle.States;
using RussSurvivor.Runtime.Gameplay.Common.Player;
using UniRx;
using UnityEngine;
using Zenject;

namespace RussSurvivor.Runtime.Gameplay.Common.Transitions
{
  [RequireComponent(typeof(Collider2D))]
  public class GateBehaviour : MonoBehaviour
  {
    [SerializeField] private GameObject _pathBlocker;

    [InjectOptional] private IBattleStateMachine _battleStateMachine;
    [InjectOptional] private IEnemyRegistry _enemyRegistry;
    private IGameplayTransitionService _transitionService;

    [Inject]
    private void Construct(IGameplayTransitionService transitionService)
    {
      _transitionService = transitionService;
    }

    private void Awake()
    {
      if (_pathBlocker != null)
        _pathBlocker.SetActive(false);
      if (_battleStateMachine != null && _enemyRegistry != null)
      {
        _battleStateMachine.CurrentState
          .Where(k => k is EndSpawningState)
          .Subscribe(_ =>
          {
            if (_enemyRegistry.AllEnemiesDead && _enemyRegistry.NoBoss)
              _pathBlocker.SetActive(false);
          })
          .AddTo(this);

        _battleStateMachine.CurrentState
          .Where(k => k is MainBattleState)
          .Subscribe(_ => _pathBlocker.SetActive(true))
          .AddTo(this);
      }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      if (other.TryGetComponent(out PlayerBehaviourBase _))
        _transitionService.GoThroughGates();
    }
  }
}