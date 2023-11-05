using System;
using System.Collections.Generic;

namespace RussSurvivor.Runtime.Gameplay.Battle.Weapons.Registry
{
  public class WeaponRegistry : IWeaponRegistry
  {
    public void Initialize()
    {
    }

    public IEnumerable<Guid> GetWeaponIds()
    {
      yield break;
    }

    public void RegisterWeapon(Guid weaponId)
    {
    }

    public void UnregisterWeapon(Guid weaponId)
    {
    }
  }
}