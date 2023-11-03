using RussSurvivor.Runtime.Gameplay.Battle.Weapons.Target;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Battle.Environment.Obstacles
{
  public class ObstacleBehaviour : MonoBehaviour, ITarget
  {
    public Vector3 Position => transform.position;
  }
}