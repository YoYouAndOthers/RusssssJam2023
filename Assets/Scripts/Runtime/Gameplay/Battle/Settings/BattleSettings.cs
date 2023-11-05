using System;
using System.Collections.Generic;
using System.Linq;

namespace RussSurvivor.Runtime.Gameplay.Battle.Settings
{
  public struct BattleSettings
  {
    public EnemyWaveSettings[] Waves;
    public float MainStateDuration;
    public float EndSpawnDuration;
    public float BossStateDuration;

    public static BattleSettings WithWaves(BattleSettings setting, EnemyWaveSettings[] waves)
    {
      return new BattleSettings
      {
        Waves = setting.Waves.Concat(waves).ToArray(),
        MainStateDuration = setting.MainStateDuration,
        EndSpawnDuration = setting.EndSpawnDuration,
        BossStateDuration = setting.BossStateDuration
      };
    }

    public static BattleSettings DefaultEmpty = new()
    {
      MainStateDuration = 120,
      EndSpawnDuration = 30,
      BossStateDuration = 180,
      Waves = new[]
      {
        new EnemyWaveSettings(enemyNumbersByType: new Dictionary<Guid, int>())
      }
    };
  }
}