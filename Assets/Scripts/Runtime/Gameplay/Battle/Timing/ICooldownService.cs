namespace RussSurvivor.Runtime.Gameplay.Battle.Timing
{
  public interface ICooldownService
  {
    void RegisterUpdatable(ICooldownUpdatable updatable);
    void UnregisterUpdatable(ICooldownUpdatable updatable);
    void PerformTick(float deltaTime);
  }
}