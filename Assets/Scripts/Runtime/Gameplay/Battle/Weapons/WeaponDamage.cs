using System;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Battle.Weapons
{
  [Serializable]
  public struct WeaponDamage
  {
    [Tooltip("How damage will be applied to target")]
    public DamageApplyType DamageApplyType;
    public float Value;
    public float DamageOverTimeRate;
    public float DamageOverTimeDuration;
  }
}