using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Battle.Weapons.Target
{
  public interface ITargetDirectionPickStrategy
  {
    public bool Get(out Vector3 direction);
  }
}