using System.Collections.Generic;
using System.Linq;
using RussSurvivor.Runtime.Gameplay.Battle.Enemies;

namespace RussSurvivor.Runtime.Gameplay.Battle.Settings
{
  public struct BattleSettings
  {
    public static BattleSettings Default = new()
    {
      EndSpawnDuration = 30,
      BossStateDuration = 180,
      Waves = new[]
      {
        new EnemyWaveSettings
          { SpawnDuration = 10, EnemyNumbersByType = new Dictionary<EnemyType, int> { [EnemyType.Lizard] = 5 } },
        new EnemyWaveSettings
          { SpawnDuration = 10, EnemyNumbersByType = new Dictionary<EnemyType, int> { [EnemyType.Lizard] = 7 } },
        new EnemyWaveSettings
          { SpawnDuration = 10, EnemyNumbersByType = new Dictionary<EnemyType, int> { [EnemyType.Lizard] = 10 } },
      }
    };

    public float BossStateDuration;
    public float EndSpawnDuration;
    public EnemyWaveSettings[] Waves;

    public BattleSettings(BattleSettings settingsForBattle)
    {
      EndSpawnDuration = settingsForBattle.EndSpawnDuration;
      BossStateDuration = settingsForBattle.BossStateDuration;
      Waves = settingsForBattle.Waves;
    }

    public static BattleSettings WithWaves(BattleSettings setting, EnemyWaveSettings[] waves)
    {
      return new BattleSettings
      {
        Waves = setting.Waves.Concat(waves).ToArray(),
        EndSpawnDuration = setting.EndSpawnDuration,
        BossStateDuration = setting.BossStateDuration
      };
    }
  }
}