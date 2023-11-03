using System;
using RussSurvivor.Runtime.Gameplay.Battle.Weapons.Damage;
using RussSurvivor.Runtime.Gameplay.Battle.Weapons.Target;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Battle.Weapons
{
  public abstract class WeaponBehaviourBase : MonoBehaviour
  {
    private IWeaponOwner _owner;
    public Guid Id { get; set; }
    public Guid TypeId { get; protected set; }
    public ITargetDirectionPickStrategy TargetDirectionPickStrategy { get; protected set; }
    public IDamageMaker DamageMaker { get; protected set; }

    public void Initialize(WeaponConfig config, IWeaponOwner owner, ITargetDirectionPickStrategy targetDirectionPickStrategy, IDamageMaker damageMaker)
    {
      _owner = owner;
      TargetDirectionPickStrategy = targetDirectionPickStrategy;
      DamageMaker = damageMaker;
      TypeId = config.Id;
      Id = Guid.NewGuid();
    }
  }
}