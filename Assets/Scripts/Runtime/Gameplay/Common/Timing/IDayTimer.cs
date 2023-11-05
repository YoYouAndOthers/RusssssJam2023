using UniRx;

namespace RussSurvivor.Runtime.Gameplay.Common.Timing
{
  public interface IDayTimer : ICooldownUpdatable
  {
    bool IsRunning { get; }
    DayTime CurrentDayTime { get; }
    FloatReactiveProperty TimeLeftReactiveProperty { get; }
  }
}