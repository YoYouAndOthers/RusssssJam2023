using RussSurvivor.Runtime.Gameplay.Battle.Weapons.Damage;
using RussSurvivor.Runtime.Gameplay.Battle.Weapons.Target;
using Zenject;

namespace RussSurvivor.Runtime.Gameplay.Battle.Weapons
{
  public class WeaponFactory
  {
    private readonly IInstantiator _instantiator;
    private readonly ClosestTargetPickerFactory _closestTargetPickerFactory;

    public WeaponFactory(IInstantiator instantiator, ClosestTargetPickerFactory closestTargetPickerFactory)
    {
      _instantiator = instantiator;
      _closestTargetPickerFactory = closestTargetPickerFactory;
    }

    public WeaponBehaviourBase Create(WeaponConfig config, IWeaponOwner owner)
    {
      var weapon = _instantiator.InstantiatePrefabForComponent<WeaponBehaviourBase>(config.Prefab, owner.WeaponsContainer);
      ITargetDirectionPickStrategy targetDirectionPickStrategy = CreateTargetPickStrategy(config, owner, weapon);
      IDamageMaker damageMaker = CreateDamageMaker(config.Damage);
      weapon.Initialize(config, owner, targetDirectionPickStrategy, damageMaker);
      return weapon;
    }

    private IDamageMaker CreateDamageMaker(WeaponDamage damage)
    {
      switch (damage.DamageApplyType)
      {
        case DamageApplyType.InstantValue:
          return new ValueDamage(damage.Value);
        default:
          return null;
      }
    }

    private ITargetDirectionPickStrategy CreateTargetPickStrategy(WeaponConfig config, ITarget owner,
      WeaponBehaviourBase weapon)
    {
      switch (config.DamageDirectionType)
      {
        case DamageDirectionType.ClosestToUser:
          ClosestTargetPicker closestTargetPicker = _closestTargetPickerFactory.Create(weapon.transform);
          closestTargetPicker.Initialize(owner, config.Reach, config.DamagableLayers);
          return new ClosestTargetDirectionPickStrategy(owner, closestTargetPicker);
        default:
          return null;
      }
    }
  }
}