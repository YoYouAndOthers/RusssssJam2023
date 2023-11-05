using RussSurvivor.Runtime.Gameplay.Battle.Weapons;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Common.Player
{
  public class PlayerBattleBehaviour : PlayerBehaviourBase, IWeaponOwner
  {
    [field: SerializeField] public WeaponConfig Fists { get; private set; }
    [field: SerializeField] public Transform WeaponsContainer { get; private set; }
    public Vector3 Position => transform.position;
    private IPlayerWeaponService _playerWeaponService;
  }
}