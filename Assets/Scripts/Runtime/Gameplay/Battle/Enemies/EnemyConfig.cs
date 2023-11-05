using System;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Battle.Enemies
{
  [CreateAssetMenu(fileName = "EnemyConfig", menuName = "RussSurvivor/Gameplay/Battle/EnemyConfig")]
  public class EnemyConfig : ScriptableObject
  {
    public Guid Id = Guid.NewGuid();
    public float MaxHealth;
    public float RegenerationPerSec;
  }
}