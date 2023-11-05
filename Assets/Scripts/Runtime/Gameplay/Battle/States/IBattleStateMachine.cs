using UniRx;

namespace RussSurvivor.Runtime.Gameplay.Battle.States
{
  public interface IBattleStateMachine
  {
    IReactiveProperty<IBattleState> CurrentState { get; }
    void SetState<TState>() where TState : class, IBattleState;
  }
}