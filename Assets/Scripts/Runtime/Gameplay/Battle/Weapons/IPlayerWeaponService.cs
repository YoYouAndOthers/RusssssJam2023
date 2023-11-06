using System;
using RussSurvivor.Runtime.Gameplay.Common.Timing;
using Zenject;

namespace RussSurvivor.Runtime.Gameplay.Battle.Weapons
{
  public interface IPlayerWeaponService : IInitializable, ICooldownUpdatable, IDisposable
  {
    void ClearWeapons();
  }
}