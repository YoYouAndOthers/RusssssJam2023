using RussSurvivor.Runtime.Gameplay.Common.Player;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace RussSurvivor.Runtime.Gameplay.Battle.Enemies
{
  public class EnemyBehaviour : MonoBehaviour
  {
    [SerializeField] private NavMeshAgent _agent;
    private IEnemyRegistry _enemyRegistry;

    private IPlayerRegistry _playerRegistry;

    [Inject]
    private void Construct(IPlayerRegistry playerRegistry, IEnemyRegistry enemyRegistry)
    {
      _playerRegistry = playerRegistry;
      _enemyRegistry = enemyRegistry;
    }

    private void Awake()
    {
      _agent.updateRotation = false;
      _agent.updateUpAxis = false;
    }

    private void Update()
    {
      PlayerBehaviourBase playerBehaviourBase = _playerRegistry.GetPlayer();
      if (playerBehaviourBase == null)
        return;
      _agent.SetDestination(playerBehaviourBase.transform.position);
    }
  }
}