using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Battle.Enemies
{
  [CreateAssetMenu(fileName = "EnemyConfig", menuName = "RussSurvivor/Gameplay/Battle/EnemyConfig")]
  public class EnemyConfig : ScriptableObject
  {
    public EnemyType Type;
    public float MaxHealth;
    public float RegenerationPerSec;
    public int Damage;
    public float DamageDelay;
    public EnemyBehaviour Prefab;
  }
}