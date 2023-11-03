using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Battle.Weapons
{
  public interface IWeaponCarrier
  {
    Transform WeaponContainer { get; }
  }
}