using System;
using System.Collections.Generic;

namespace RussSurvivor.Runtime.Gameplay.Battle.Weapons.Registry
{
  public class WeaponRegistry : IWeaponRegistry
  {
    private readonly List<Guid> _weaponIds = new();

    public void Initialize()
    {
    }

    public IEnumerable<Guid> GetWeaponIds()
    {
      return _weaponIds;
    }

    public void RegisterWeapon(Guid weaponId)
    {
      _weaponIds.Add(weaponId);
    }

    public void UnregisterWeapon(Guid weaponId)
    {
      _weaponIds.Remove(weaponId);
    }
  }
}