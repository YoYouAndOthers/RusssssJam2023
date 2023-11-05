using System;
using System.Collections.Generic;
using Zenject;

namespace RussSurvivor.Runtime.Gameplay.Battle.Weapons.Registry
{
  public interface IWeaponRegistry : IInitializable
  {
    IEnumerable<Guid> GetWeaponIds();
    void RegisterWeapon(Guid weaponId);
    void UnregisterWeapon(Guid weaponId);
  }
}