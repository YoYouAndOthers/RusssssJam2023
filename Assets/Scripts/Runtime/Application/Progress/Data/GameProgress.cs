using System;

namespace RussSurvivor.Runtime.Application.Progress.Data
{
  [Serializable]
  public class GameProgress
  {
    public int Version { get; set; } = 1;
    public Vector2Data PlayerPosition { get; set; } = Vector2Data.Zero;
  }

  [Serializable]
  public struct Vector2Data
  {
    public float x;
    public float y;

    public static Vector2Data Zero => new()
    {
      x = 0,
      y = 0
    };
  }
}