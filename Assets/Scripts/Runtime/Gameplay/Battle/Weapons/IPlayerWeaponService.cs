using System.Collections.Generic;

namespace RussSurvivor.Runtime.Gameplay.Battle.Weapons
{
  public interface IPlayerWeaponService
  {
    IEnumerable<WeaponBehaviourBase> Weapons { get; }
    void Add(WeaponBehaviourBase weapon);
    void Remove(WeaponBehaviourBase weapon);
  }
}