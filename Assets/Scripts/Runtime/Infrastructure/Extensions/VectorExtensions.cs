using UnityEngine;

namespace RussSurvivor.Runtime.Infrastructure.Extensions
{
  public static class VectorExtensions
  {
    public static Vector3 RandomOnRing(this Vector3 vector, float innerRadius, float outerRadius)
    {
      float randomRadius = Random.Range(innerRadius, outerRadius);
      var resultRadius = 0f;
      Vector2 resultVector = Vector2.zero;
      while (resultRadius < innerRadius || resultRadius > outerRadius)
      {
        resultVector = randomRadius * Random.insideUnitCircle;
        resultRadius = resultVector.magnitude;
      }

      return vector + resultVector.WithZ(vector.z);
    }

    public static Vector2 RandomOnRing(this Vector2 vector, float innerRadius, float outerRadius)
    {
      float randomRadius = Random.Range(innerRadius, outerRadius);
      return vector + randomRadius * Random.insideUnitCircle;
    }

    public static Vector3 WithX(this Vector3 vector, float x)
    {
      return new Vector3(x, vector.y, vector.z);
    }

    public static Vector3 WithY(this Vector3 vector, float y)
    {
      return new Vector3(vector.x, y, vector.z);
    }

    public static Vector3 WithZ(this Vector3 vector, float z)
    {
      return new Vector3(vector.x, vector.y, z);
    }

    public static Vector3 WithZ(this Vector2 vector, float z)
    {
      return new Vector3(vector.x, vector.y, z);
    }
  }
}