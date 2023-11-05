namespace RussSurvivor.Runtime.Gameplay.Common.Timing
{
  public interface ICooldownService
  {
    void RegisterUpdatable(ICooldownUpdatable updatable);
    void UnregisterUpdatable(ICooldownUpdatable updatable);
    void PerformTick(float deltaTime);
  }
}