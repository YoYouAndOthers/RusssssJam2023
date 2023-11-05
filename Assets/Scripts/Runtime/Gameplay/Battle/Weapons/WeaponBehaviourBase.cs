using System;
using RussSurvivor.Runtime.Gameplay.Battle.Weapons.Damage;
using RussSurvivor.Runtime.Gameplay.Battle.Weapons.Target;
using RussSurvivor.Runtime.Gameplay.Common.Timing;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Battle.Weapons
{
  public abstract class WeaponBehaviourBase : MonoBehaviour, ICooldownUpdatable
  {
    public float TimeLeft { get; }

    public bool IsReady { get; protected set; }
    protected bool IsPerforming { get; set; }
    private float CooldownTime { get; set; }

    public Guid Id { get; set; }

    public Guid TypeId { get; protected set; }

    public ITargetDirectionPickStrategy TargetDirectionPickStrategy { get; protected set; }

    public IDamageMaker DamageMaker { get; protected set; }

    private float _timer;

    public void UpdateCooldown(float deltaTime)
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
    }

    public virtual void Initialize(WeaponConfig config, ITargetDirectionPickStrategy targetDirectionPickStrategy,
      IDamageMaker damageMaker)
    {
      TargetDirectionPickStrategy = targetDirectionPickStrategy;
      DamageMaker = damageMaker;
      TypeId = config.Id;
      Id = Guid.NewGuid();
      CooldownTime = config.InitialCooldown;
      name = config.Name;
    }

    public virtual bool ReadyToPerform(out Vector3 direction)
    {
      bool hasTarget = TargetDirectionPickStrategy.Get(out direction);
      Debug.Log($"Weapon {name} ready to perform: {IsReady} has target: {hasTarget} is performing: {IsPerforming}");

      return !IsPerforming && IsReady && hasTarget;
    }

    public virtual void Perform(Vector3 direction)
    {
      IsPerforming = true;
      IsReady = false;
    }
  }
}