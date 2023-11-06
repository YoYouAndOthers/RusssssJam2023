using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using RussSurvivor.Runtime.Infrastructure.Extensions;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace RussSurvivor.Runtime.Gameplay.Battle.Weapons.Content
{
  public class WeaponConfigProvider : IWeaponConfigProvider
  {
    private HashSet<WeaponConfig> _weapons = new();
    private Dictionary<Guid, WeaponConfig> _weaponsById;

    public async UniTask InitializeAsync()
    {
      Debug.Log("Weapon registry initialized");
      IList<WeaponConfig> weapons = await Addressables.LoadAssetsAsync<WeaponConfig>(new List<string> { "Weapons" },
        null,
        Addressables.MergeMode.Intersection);
      _weaponsById = weapons.ToDictionary(config => config.Id);
      _weapons = new HashSet<WeaponConfig>(weapons);
    }

    public bool TryGetWeaponConfig(Guid weaponId, out WeaponConfig config)
    {
      return _weaponsById.TryGetValue(weaponId, out config);
    }

    public IEnumerable<WeaponConfig> GetRandomWeaponTypesToSell(int count)
    {
      return _weapons.Where(k => k.CanBeTraded).RandomElements(count);
    }
  }
}