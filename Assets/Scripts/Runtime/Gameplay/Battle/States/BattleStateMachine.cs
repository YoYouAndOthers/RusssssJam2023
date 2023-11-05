using System;
using System.Collections.Generic;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Battle.States
{
  public class BattleStateMachine : IBattleStateMachine
  {
    private readonly Dictionary<Type, IBattleState> _states = new()
    {
      [typeof(MainBattleState)] = new MainBattleState(),
      [typeof(EndSpawningState)] = new EndSpawningState(),
      [typeof(BossState)] = new BossState(),
      [typeof(EndBattleState)] = new EndBattleState()
    };

    public IBattleState CurrentState { get; private set; }

    public void SetState<TState>() where TState : class, IBattleState
    {
      Debug.Log($"Changing state to {typeof(TState)}");
      var state = ChangeState<TState>();
      state.Enter();
    }

    private TState ChangeState<TState>() where TState : class, IBattleState
    {
      CurrentState?.Exit();
      CurrentState = _states[typeof(TState)] as TState;
      return _states[typeof(TState)] as TState;
    }
  }
}