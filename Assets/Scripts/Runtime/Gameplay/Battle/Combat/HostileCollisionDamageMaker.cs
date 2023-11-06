using RussSurvivor.Runtime.Gameplay.Battle.Enemies;
using RussSurvivor.Runtime.Gameplay.Battle.Weapons.Damage;
using RussSurvivor.Runtime.Gameplay.Common.Player;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Battle.Combat
{
  [RequireComponent(typeof(Collider2D))]
  public class HostileCollisionDamageMaker : MonoBehaviour, IDamageMaker
  {
    [SerializeField] private EnemyBehaviour _owner;
    private float _currentDamageTime;
    
    public bool TryApply(IDamagable damagable)
    {
      return damagable.TryTakeDamage(_owner.Damage);
    }

    public void SetValue(float value)
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      if (other.attachedRigidbody.TryGetComponent(out PlayerBattleBehaviour player))
      {
        _currentDamageTime = 0;
      }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
      if (other.attachedRigidbody.TryGetComponent(out PlayerBattleBehaviour player))
      {
        _currentDamageTime += Time.deltaTime;
        if (_currentDamageTime > _owner.Delay)
        {
          TryApply(player);
          _currentDamageTime = 0;
        }
      }
    }
  }
}