using System.Linq;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Battle.Settings
{
  public class BattleSettingsService : IBattleSettingsService
  {
    public BattleSettings SettingsForBattle { get; private set; } = BattleSettings.Default;

    public void AddWaveToNext(EnemyWaveSettings waveSettings)
    {
      SettingsForBattle = new BattleSettings(SettingsForBattle)
      {
        Waves = SettingsForBattle.Waves.Concat(new[] { waveSettings }).ToArray()
      };
    }

    public void AddMainStateDuration(float duration)
    {
      if (!(duration > 0))
      {
        Debug.LogWarning("Duration must be positive");
        return;
      }

      EnemyWaveSettings lastWave = SettingsForBattle.Waves.Last();
      lastWave.SpawnDuration += duration;
      SettingsForBattle.Waves[^1] = lastWave;
    }

    public void ChangeEscapeTime(float escapeTime)
    {
      SettingsForBattle = new BattleSettings(SettingsForBattle)
      {
        EndSpawnDuration = escapeTime
      };
    }

    public void ChangeBossTime(float bossTime)
    {
      SettingsForBattle = new BattleSettings(SettingsForBattle)
      {
        BossStateDuration = bossTime
      };
    }
  }
}