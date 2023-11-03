using RussSurvivor.Runtime.Gameplay.Battle.Weapons;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Common.Player
{
  public class PlayerBattleBehaviour : MonoBehaviour, IWeaponOwner
  {
    [SerializeField] private PlayerMovement _playerMovement;
    [field: SerializeField] public Transform WeaponsContainer { get; private set; }
    public Vector3 Position => transform.position;
  }
}