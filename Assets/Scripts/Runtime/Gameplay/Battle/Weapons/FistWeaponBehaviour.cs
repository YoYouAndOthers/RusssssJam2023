using RussSurvivor.Runtime.Gameplay.Battle.Weapons.Damage;
using RussSurvivor.Runtime.Gameplay.Battle.Weapons.Target;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Battle.Weapons
{
  public class FistWeaponBehaviour : WeaponBehaviourBase
  {
    [SerializeField] private Fist _fists;
    private float _reach;

    public override void Initialize(WeaponConfig config,
      ITargetDirectionPickStrategy targetDirectionPickStrategy,
      IDamageMaker damageMaker)
    {
      Debug.Log("Fists initialized");
      base.Initialize(config, targetDirectionPickStrategy, damageMaker);
      _fists.Initialize(config.DamagableLayers, config.WeaponStats[WeaponStatType.Piercing]);
      _reach = config.Reach;
    }

    public override async void Perform(Vector3 direction)
    {
      Debug.Log("Fists perform");
      base.Perform(direction);
      await _fists.MakeHit(direction, _reach, DamageMaker);
      IsPerforming = false;
    }
  }
}