using RussSurvivor.Runtime.Gameplay.Common.Player;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace RussSurvivor.Runtime.Gameplay.Battle.Enemies
{
  public class WalkableEnemyBehaviour : EnemyBehaviour
  {
    [SerializeField] private NavMeshAgent _agent;

    [Inject] private IPlayerRegistry _playerRegistry;

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