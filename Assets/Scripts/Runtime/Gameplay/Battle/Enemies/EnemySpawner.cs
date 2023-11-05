using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Battle.Enemies
{
  public class EnemySpawner : MonoBehaviour
  {
    [SerializeField] private Transform _corner0;
    [SerializeField] private Transform _corner1;
    private float _maxX;
    private float _maxY;

    private float _minX;
    private float _minY;

    private void OnDrawGizmos()
    {
      Gizmos.color = new Color(1, 0, 0, 0.5f);
      if (_corner0 == null || _corner1 == null) return;
      Vector3 center = (_corner0.position + _corner1.position) / 2;
      Gizmos.DrawCube(center, _corner1.position - _corner0.position);
    }

    public void Initialize()
    {
      _minY = Mathf.Min(_corner0.position.y, _corner1.position.y);
      _maxX = Mathf.Max(_corner0.position.x, _corner1.position.x);
      _maxY = Mathf.Max(_corner0.position.y, _corner1.position.y);
      _minX = Mathf.Min(_corner0.position.x, _corner1.position.x);
    }

    public Vector3 GetRandomPosition()
    {
      return new Vector3(Random.Range(_minX, _maxX), Random.Range(_minY, _maxY));
    }
  }
}