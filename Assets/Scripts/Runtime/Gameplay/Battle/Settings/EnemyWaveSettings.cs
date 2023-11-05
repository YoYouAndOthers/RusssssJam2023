using System.Collections.Generic;
using RussSurvivor.Runtime.Gameplay.Battle.Enemies;

namespace RussSurvivor.Runtime.Gameplay.Battle.Settings
{
  public struct EnemyWaveSettings
  {
    public Dictionary<EnemyType, int> EnemyNumbersByType;
    public float SpawnDuration;
  }
}