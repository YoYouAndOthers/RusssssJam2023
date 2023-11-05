using System.Collections.Generic;
using Zenject;

namespace RussSurvivor.Runtime.Gameplay.Battle.Weapons.Registry
{
  public interface IWeaponRegistry : IInitializable
  {
    IEnumerable<WeaponConfig> Weapons { get; }
    void Add(WeaponConfig weapon);
    void Remove(WeaponConfig weapon);
  }
}