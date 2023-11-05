using System.Collections.Generic;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Battle.Weapons.Registry
{
  public class WeaponRegistry : IWeaponRegistry
  {
    public IEnumerable<WeaponConfig> Weapons => _weapons;
    private List<WeaponConfig> _weapons;

    public void Initialize()
    {
      Debug.Log("Weapon registry initialized");
      _weapons = new List<WeaponConfig>();
    }

    public void Add(WeaponConfig weapon)
    {
      Debug.Log($"Weapon added: {weapon}");
      _weapons.Add(weapon);
    }

    public void Remove(WeaponConfig weapon)
    {
      _weapons.Remove(weapon);
    }
  }
}