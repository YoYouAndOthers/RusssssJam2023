using RussSurvivor.Runtime.Gameplay.Common.Timing;

namespace RussSurvivor.Runtime.Gameplay.Battle.Timing
{
  public interface IBattleTimer : ICooldownUpdatable
  {
    public void Initialize(float battleTimeLeft, float escapeTimeLeft, float bossTimeLeft);
  }
}