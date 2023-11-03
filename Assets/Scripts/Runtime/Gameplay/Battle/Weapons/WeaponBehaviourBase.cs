using System;
using RussSurvivor.Runtime.Gameplay.Battle.Weapons.Damage;
using RussSurvivor.Runtime.Gameplay.Battle.Weapons.Target;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Battle.Weapons
{
  public abstract class WeaponBehaviourBase : MonoBehaviour
  {
    protected bool IsPerforming { get; set; }
    private float CooldownTime { get; set; }
    public bool IsReady { get; protected set; }
    public Guid Id { get; set; }
    public Guid TypeId { get; protected set; }
    public ITargetDirectionPickStrategy TargetDirectionPickStrategy { get; protected set; }
    public IDamageMaker DamageMaker { get; protected set; }
    private IWeaponOwner _owner;
    private float _timer;

    public virtual void Initialize(WeaponConfig config, IWeaponOwner owner, ITargetDirectionPickStrategy targetDirectionPickStrategy, IDamageMaker damageMaker)
    {
      _owner = owner;
      TargetDirectionPickStrategy = targetDirectionPickStrategy;
      DamageMaker = damageMaker;
      TypeId = config.Id;
      Id = Guid.NewGuid();
      CooldownTime = config.InitialCooldown;
      name = config.Name;
    }

    public void UpdateCoolDown(float deltaTime)
    {
      if (IsPerforming)
        return;

      if (_timer < CooldownTime && !IsReady)
      {
        _timer += deltaTime;
      }
      else
      {
        IsReady = true;
        _timer = 0;
      }

      if (IsReady && TargetDirectionPickStrategy.Get(out Vector3 direction))
        Perform(direction);
    }

    protected virtual void Perform(Vector3 direction)
    {
      IsPerforming = true;
      IsReady = false;
    }
  }
}