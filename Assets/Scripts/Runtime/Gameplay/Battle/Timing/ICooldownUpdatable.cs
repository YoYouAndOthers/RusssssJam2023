namespace RussSurvivor.Runtime.Gameplay.Battle.Timing
{
  public interface ICooldownUpdatable
  {
    void UpdateCooldown(float deltaTime);
    float TimeLeft { get; }
    bool IsReady { get; }
  }
}