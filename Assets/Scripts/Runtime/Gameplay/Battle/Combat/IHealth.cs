using RussSurvivor.Runtime.Gameplay.Common.Timing;
using UniRx;

namespace RussSurvivor.Runtime.Gameplay.Battle.Combat
{
  public interface IHealth : ICooldownUpdatable
  {
    public FloatReactiveProperty CurrentHealth { get; }
    public float MaxHealth { get; }
    public float RegenerationPerSec { get; }
  }
}