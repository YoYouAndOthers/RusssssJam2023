using System;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Battle.Weapons
{
  [CreateAssetMenu(menuName = "RussSurvivor/Gameplay/Weapons/WeaponConfig", fileName = "Weapon_WeaponName")]
  public class WeaponConfig : ScriptableObject
  {
    public Guid Id = Guid.NewGuid();
    public string Name;
    public string Description;
    [Tooltip("Cooldown without modifiers in seconds")] public float InitialCooldown;
    public WeaponDamage Damage;
    [Tooltip("Direction of weapon attack")] public DamageDirectionType DamageDirectionType;
    public float BaseSize;
    public WeaponBehaviourBase Prefab;
    public SerializedDictionary<WeaponEffectType, float> WeaponStats = new();
    public int DamagableLayers;

    public bool IsAoE()
    {
      return DamageDirectionType is
        DamageDirectionType.AoEOnClosest or
        DamageDirectionType.AoEOnRandom or
        DamageDirectionType.AoEByMovement;
    }
  }
}