using System;

namespace RussSurvivor.Runtime.Application.Progress.Data
{
  [Serializable]
  public class GameProgress
  {
    public int Version { get; set; } = 1;
    public string Test { get; set; } = "Test";
  }
}