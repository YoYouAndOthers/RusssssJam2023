using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Battle.Environment.Obstacles
{
  [CreateAssetMenu(menuName = "RussSurvivor/Gameplay/Battle/ObstaclesConfig")]
  public class ObstaclesConfig : ScriptableObject
  {
    public ObstacleBehaviour Prefab;
    public int Number;
    public float Radius;
  }
}