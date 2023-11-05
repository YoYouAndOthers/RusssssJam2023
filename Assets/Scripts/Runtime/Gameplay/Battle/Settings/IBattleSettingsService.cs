namespace RussSurvivor.Runtime.Gameplay.Battle.Settings
{
  public interface IBattleSettingsService
  {
    BattleSettings SettingsForBattle { get; }
    void AddWaveToNext(EnemyWaveSettings waveSettings);
    void AddMainStateDuration(float duration);
    void ChangeEscapeTime(float escapeTime);
    void ChangeBossTime(float bossTime);
  }
}