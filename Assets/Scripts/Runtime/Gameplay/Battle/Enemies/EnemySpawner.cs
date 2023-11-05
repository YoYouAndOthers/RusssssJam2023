using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Battle.Enemies
{
  public class EnemySpawner : MonoBehaviour
  {
    [SerializeField] private Transform _corner0;
    [SerializeField] private Transform _corner1;

    public void Initialize()
    {
      
    }

    private void OnDrawGizmos()
    {
      Gizmos.color = new Color(1, 0, 0, 0.5f);
      if(_corner0 == null || _corner1 == null) return;
      Vector3 center = (_corner0.position + _corner1.position) / 2;
      Gizmos.DrawCube(center, _corner1.position - _corner0.position);
    }
  }
}