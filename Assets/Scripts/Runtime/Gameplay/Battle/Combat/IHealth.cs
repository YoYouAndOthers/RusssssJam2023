using RussSurvivor.Runtime.Gameplay.Common.Timing;

namespace RussSurvivor.Runtime.Gameplay.Battle.Combat
{
  public interface IHealth : ICooldownUpdatable
  {
    public float CurrentHealth { get; }
    public float MaxHealth { get; }
    public float RegenerationPerSec { get; }
  }
}