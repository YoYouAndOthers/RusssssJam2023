namespace RussSurvivor.Runtime.Gameplay.Battle.States
{
  public interface IBattleStateMachine
  {
    IBattleState CurrentState { get; }
    void SetState<TState>() where TState : class, IBattleState;
  }
}