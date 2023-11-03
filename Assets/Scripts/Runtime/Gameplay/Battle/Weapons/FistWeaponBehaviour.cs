using System.Threading;
using RussSurvivor.Runtime.Gameplay.Battle.Weapons.Damage;
using RussSurvivor.Runtime.Gameplay.Battle.Weapons.Target;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Battle.Weapons
{
  public class FistWeaponBehaviour : WeaponBehaviourBase
  {
    [SerializeField] private Fist _fists;
    private float _reach;

    public override void Initialize(WeaponConfig config, IWeaponOwner owner,
      ITargetDirectionPickStrategy targetDirectionPickStrategy,
      IDamageMaker damageMaker)
    {
      base.Initialize(config, owner, targetDirectionPickStrategy, damageMaker);
      _fists.Initialize(config.DamagableLayers, config.WeaponStats[WeaponStatType.Piercing]);
      _reach = config.Reach;
    }

    protected override async void Perform(Vector3 direction)
    {
      base.Perform(direction);
      await _fists.MakeHit(direction - transform.position, _reach, DamageMaker);
      IsPerforming = false;
    }
  }
}