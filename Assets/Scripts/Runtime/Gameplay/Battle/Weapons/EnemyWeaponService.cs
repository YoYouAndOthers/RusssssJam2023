using System.Collections.Generic;

namespace RussSurvivor.Runtime.Gameplay.Battle.Weapons
{
  public class EnemyWeaponService : IEnemyWeaponService
  {
    private IEnumerable<WeaponBehaviourBase> _weapons;

    public IEnumerable<WeaponBehaviourBase> Weapons => _weapons;

    public void Add(WeaponBehaviourBase weapon)
    {
    }

    public void Remove(WeaponBehaviourBase weapon)
    {
    }
  }
}