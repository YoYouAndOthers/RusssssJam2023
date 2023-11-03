using RussSurvivor.Runtime.Gameplay.Battle.Weapons.Damage;
using RussSurvivor.Runtime.Gameplay.Battle.Weapons.Target;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Battle.Environment.Obstacles
{
  public class ObstacleBehaviour : MonoBehaviour, ITarget, IDamagable
  {
    public Vector3 Position => transform.position;
    public bool TryTakeDamage(float damage, bool percent = false)
    {
      Kill();
      return true;
    }

    public void Kill()
    {
      Destroy(gameObject);
    }
  }
}