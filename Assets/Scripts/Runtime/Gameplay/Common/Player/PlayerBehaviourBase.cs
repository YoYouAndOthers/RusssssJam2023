using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Common.Player
{
  public abstract class PlayerBehaviourBase : MonoBehaviour
  {
    [SerializeField] private PlayerMovement _playerMovement;
  }
}