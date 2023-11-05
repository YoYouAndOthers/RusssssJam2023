using System.Collections.Generic;
using System.Linq;
using RussSurvivor.Runtime.Gameplay.Battle.Enemies;
using RussSurvivor.Runtime.Gameplay.Battle.Settings;

namespace RussSurvivor.Runtime.Gameplay.Battle.States
{
  public class MainBattleState : IBattleState
  {
    private readonly IBattleStateMachine _stateMachine;
    private readonly IBattleSettingsService _settingsService;
    private readonly IEnemyRegistry _enemyRegistry;
    private readonly EnemyFactory _enemyFactory;
    private int _currentWaveIndex;

    private float _currentWaveTime;

    public MainBattleState(
      IBattleStateMachine stateMachine,
      IBattleSettingsService settingsService,
      IEnemyRegistry enemyRegistry,
      EnemyFactory enemyFactory)
    {
      _stateMachine = stateMachine;
      _settingsService = settingsService;
      _enemyRegistry = enemyRegistry;
      _enemyFactory = enemyFactory;
    }

    public void Enter()
    {
      _currentWaveIndex = 0;
      _currentWaveTime = 0;
    }

    public void Execute(float deltaTime)
    {
      _currentWaveTime += deltaTime;
      if (_currentWaveTime >= _settingsService.SettingsForBattle.Waves[_currentWaveIndex].SpawnDuration)
      {
        _currentWaveTime = 0;
        _currentWaveIndex++;
        if (_currentWaveIndex >= _settingsService.SettingsForBattle.Waves.Length)
        {
          _stateMachine.SetState<EndSpawningState>();
          return;
        }
      }

      RefreshEnemyWave();
    }

    public void Exit()
    {
    }

    private void RefreshEnemyWave()
    {
      foreach (KeyValuePair<EnemyType, int> enemiesOfType in _settingsService.SettingsForBattle.Waves[_currentWaveIndex]
                 .EnemyNumbersByType)
        for (var i = 0; i < enemiesOfType.Value - _enemyRegistry.GetEnemiesOfType(enemiesOfType.Key).Count(); i++)
          _enemyRegistry.Add(_enemyFactory.Create(enemiesOfType.Key), enemiesOfType.Key);
    }
  }
}