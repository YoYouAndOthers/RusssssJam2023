using System.Collections.Generic;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Battle.Weapons
{
  public class PlayerWeaponService : IPlayerWeaponService
  {
    private readonly List<WeaponBehaviourBase> _weapons = new();

    public IEnumerable<WeaponBehaviourBase> Weapons => _weapons;

    public void Add(WeaponBehaviourBase weapon)
    {
      Debug.Log($"Weapon added: {weapon}");
      _weapons.Add(weapon);
    }

    public void Remove(WeaponBehaviourBase weapon)
    {
      _weapons.Remove(weapon);
    }
  }
}