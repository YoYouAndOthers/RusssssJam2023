using System.Collections.Generic;

namespace RussSurvivor.Runtime.Gameplay.Battle.Weapons
{
  public class EnemyWeaponService : IEnemyWeaponService
  {
    public IEnumerable<WeaponBehaviourBase> Weapons { get; }

    public void Add(WeaponBehaviourBase weapon)
    {
    }

    public void Remove(WeaponBehaviourBase weapon)
    {
    }
  }
}