using RussSurvivor.Runtime.Gameplay.Battle.Weapons.Target;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Battle.Weapons
{
  public interface IWeaponOwner : ITarget
  {
    Transform WeaponsContainer { get; }
  }
}