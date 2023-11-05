using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Battle.Enemies
{
  [CreateAssetMenu(fileName = "EnemyConfig", menuName = "RussSurvivor/Gameplay/Battle/EnemyConfig")]
  public class EnemyConfig : ScriptableObject
  {
    public float MaxHealth;
    public float RegenerationPerSec;
  }
}