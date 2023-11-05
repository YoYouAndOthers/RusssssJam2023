using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace RussSurvivor.Runtime.Gameplay.Battle.Weapons.Content
{
  public class WeaponConfigProvider : IWeaponConfigProvider
  {
    private Dictionary<Guid, WeaponConfig> _weapons;

    public async UniTask InitializeAsync()
    {
      Debug.Log("Weapon registry initialized");
      IList<WeaponConfig> weapons = await Addressables.LoadAssetsAsync<WeaponConfig>(new List<string>() { "Weapons" }, null,
        Addressables.MergeMode.Intersection);
      _weapons = weapons.ToDictionary(config => config.Id);
    }

    public bool TryGetWeaponConfig(Guid weaponId, out WeaponConfig config)
    {
      return _weapons.TryGetValue(weaponId, out config);
    }
  }
}