using System;
using System.Collections.Generic;

namespace RussSurvivor.Runtime.Gameplay.Battle.Settings
{
  public struct EnemyWaveSettings
  {
    public Dictionary<Guid, int> EnemyNumbersByType;

    public EnemyWaveSettings(Dictionary<Guid, int> enemyNumbersByType)
    {
      EnemyNumbersByType = enemyNumbersByType;
    }
  }
}