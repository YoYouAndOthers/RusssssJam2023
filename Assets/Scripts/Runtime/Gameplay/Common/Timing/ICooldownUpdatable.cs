namespace RussSurvivor.Runtime.Gameplay.Common.Timing
{
  public interface ICooldownUpdatable
  {
    float TimeLeft { get; }
    bool IsReady { get; }
    void UpdateCooldown(float deltaTime);
  }
}