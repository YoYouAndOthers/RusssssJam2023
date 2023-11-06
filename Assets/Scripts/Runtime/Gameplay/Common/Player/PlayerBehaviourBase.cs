using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Common.Player
{
  public abstract class PlayerBehaviourBase : MonoBehaviour
  {
    [SerializeField] protected PlayerMovement PlayerMovement;
    [SerializeField] protected PlayerDash PlayerDash;
  }
}