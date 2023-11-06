using RussSurvivor.Runtime.Gameplay.Battle.Combat;
using RussSurvivor.Runtime.Gameplay.Battle.Weapons.Damage;
using RussSurvivor.Runtime.Gameplay.Battle.Weapons.Target;
using RussSurvivor.Runtime.UI.Gameplay.Battle;
using UniRx;
using UnityEngine;
using Zenject;

namespace RussSurvivor.Runtime.Gameplay.Battle.Enemies
{
  public abstract class EnemyBehaviour : MonoBehaviour, ITarget, IHealth, IDamagable, ICollisionDamage
  {
    public float TimeLeft { get; private set; }

    public bool IsReady => CurrentHealth.Value >= MaxHealth;

    public FloatReactiveProperty CurrentHealth { get; } = new();
    
    private float CurrentHealthValue
    {
      get => _currentHealth;
      set
      {
        if (value > MaxHealth)
        {
          _currentHealth = MaxHealth;
          CurrentHealth.Value = MaxHealth;
        }
        else if (value <= 0)
        {
          _currentHealth = 0;
          CurrentHealth.Value = 0;
          Kill();
        }
        else
        {
          _currentHealth = value;
          CurrentHealth.Value = value;
        }
      }
    }

    public float MaxHealth { get; private set; }
    public float RegenerationPerSec { get; private set; }
    public Vector3 Position => gameObject != null ? transform.position + Vector3.up : Vector3.positiveInfinity;

    public int Damage { get; private set; }

    public float Delay { get; private set; }

    public EnemyType EnemyType { get; set; }
    
    private float _currentHealth;

    private IEnemyRegistry _enemyRegistry;
    private IDamageCountService _damageCountService;

    [Inject]
    private void Construct(IEnemyRegistry enemyRegistry, IDamageCountService damageCountService)
    {
      _enemyRegistry = enemyRegistry;
      _damageCountService = damageCountService;
    }

    public bool TryTakeDamage(float damage, bool percent = false)
    {
      if (percent)
        damage = damage * MaxHealth / 100;

      CurrentHealthValue -= damage;
      _damageCountService.ShowDamageCount(Position, (int)damage);
      return true;
    }

    public void Kill()
    {
      _enemyRegistry.Remove(this);
      if(gameObject != null)
        Destroy(gameObject);
    }

    public void UpdateCooldown(float deltaTime)
    {
      TimeLeft -= deltaTime;
      if (TimeLeft <= 0)
      {
        CurrentHealthValue += RegenerationPerSec * deltaTime;
        TimeLeft = 1;
      }
    }

    public void Initialize(EnemyConfig config)
    {
      EnemyType = config.Type;
      MaxHealth = config.MaxHealth;
      CurrentHealthValue = config.MaxHealth;
      RegenerationPerSec = config.RegenerationPerSec;
      Damage = config.Damage;
      Delay = config.DamageDelay;
    }
  }
}