using RussSurvivor.Runtime.Gameplay.Battle.Combat;
using RussSurvivor.Runtime.Gameplay.Battle.Weapons.Damage;
using RussSurvivor.Runtime.Gameplay.Battle.Weapons.Target;
using RussSurvivor.Runtime.Gameplay.Common.Player;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace RussSurvivor.Runtime.Gameplay.Battle.Enemies
{
  public class EnemyBehaviour : MonoBehaviour, ITarget, IHealth, IDamagable
  {
    [SerializeField] private NavMeshAgent _agent;
    private IEnemyRegistry _enemyRegistry;
    private IPlayerRegistry _playerRegistry;

    private float _currentHealth;
    private float _timeBeforeRegen;
    public Vector3 Position => transform.position + Vector3.up;
    public float TimeLeft => _timeBeforeRegen;
    public bool IsReady => CurrentHealth >= MaxHealth;

    public float CurrentHealth
    {
      get => _currentHealth;
      private set
      {
        if (value > MaxHealth)
          _currentHealth = MaxHealth;
        else if (value < 0)
        {
          _currentHealth = 0;
          Kill();
        }
        else
          _currentHealth = value;
      }
    }

    public float MaxHealth { get; private set; }
    public float RegenerationPerSec { get; private set; }

    [Inject]
    private void Construct(IPlayerRegistry playerRegistry, IEnemyRegistry enemyRegistry)
    {
      _playerRegistry = playerRegistry;
      _enemyRegistry = enemyRegistry;
    }

    public void Initialize(EnemyConfig config)
    {
      MaxHealth = config.MaxHealth;
      CurrentHealth = config.MaxHealth;
      RegenerationPerSec = config.RegenerationPerSec;
      _enemyRegistry.Add(this);
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

    public bool TryTakeDamage(float damage, bool percent = false)
    {
      if (percent)
        damage = damage * MaxHealth / 100;

      CurrentHealth -= damage;
      return true;
    }

    public void Kill()
    {
      _enemyRegistry.Remove(this);
      Destroy(gameObject);
    }

    public void UpdateCooldown(float deltaTime)
    {
      _timeBeforeRegen -= deltaTime;
      if (_timeBeforeRegen <= 0)
      {
        CurrentHealth += RegenerationPerSec * deltaTime;
        _timeBeforeRegen = 1;
      }
    }
  }
}