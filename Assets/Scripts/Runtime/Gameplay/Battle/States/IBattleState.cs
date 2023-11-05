namespace RussSurvivor.Runtime.Gameplay.Battle.States
{
  public interface IBattleState
  {
    void Enter();
    void Exit();
    void Execute(float deltaTime);
  }
}