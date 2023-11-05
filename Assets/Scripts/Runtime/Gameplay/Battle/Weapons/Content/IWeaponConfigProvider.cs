using System;
using Cysharp.Threading.Tasks;

namespace RussSurvivor.Runtime.Gameplay.Battle.Weapons.Content
{
  public interface IWeaponConfigProvider
  {
    UniTask InitializeAsync();
    bool TryGetWeaponConfig(Guid weaponId, out WeaponConfig config);
  }
}