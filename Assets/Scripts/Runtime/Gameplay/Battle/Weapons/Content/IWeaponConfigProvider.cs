using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace RussSurvivor.Runtime.Gameplay.Battle.Weapons.Content
{
  public interface IWeaponConfigProvider
  {
    UniTask InitializeAsync();
    bool TryGetWeaponConfig(Guid weaponId, out WeaponConfig config);
    IEnumerable<WeaponConfig> GetRandomWeaponTypesToSell(int count);
  }
}