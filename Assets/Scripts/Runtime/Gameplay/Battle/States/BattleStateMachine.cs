using System;
using System.Collections.Generic;
using RussSurvivor.Runtime.Gameplay.Battle.Enemies;
using RussSurvivor.Runtime.Gameplay.Battle.Settings;
using UniRx;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Battle.States
{
  public class BattleStateMachine : IBattleStateMachine
  {
    private readonly Dictionary<Type, IBattleState> _states;

    private readonly ReactiveProperty<IBattleState> _currentBattleState = new();

    public IReactiveProperty<IBattleState> CurrentState => _currentBattleState;

    public BattleStateMachine(IBattleSettingsService battleSettingsService, IEnemyRegistry enemyRegistry,
      EnemyFactory enemyFactory) =>
      _states = new Dictionary<Type, IBattleState>
      {
        [typeof(MainBattleState)] = new MainBattleState(this, battleSettingsService, enemyRegistry, enemyFactory),
        [typeof(EndSpawningState)] = new EndSpawningState(),
        [typeof(BossState)] = new BossState(),
        [typeof(EndBattleState)] = new EndBattleState()
      };

    public void SetState<TState>() where TState : class, IBattleState
    {
      Debug.Log($"Changing state to {typeof(TState)}");
      var state = ChangeState<TState>();
      state.Enter();
    }

    private TState ChangeState<TState>() where TState : class, IBattleState
    {
      _currentBattleState?.Value?.Exit();
      if (_currentBattleState != null)
        _currentBattleState.Value = _states[typeof(TState)] as TState;
      return _states[typeof(TState)] as TState;
    }
  }
}