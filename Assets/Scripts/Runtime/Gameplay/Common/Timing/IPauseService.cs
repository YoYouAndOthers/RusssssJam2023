using UniRx;

namespace RussSurvivor.Runtime.Gameplay.Common.Timing
{
  public interface IPauseService
  {
    BoolReactiveProperty IsPaused { get; }
    void Pause();
    void Resume();
  }
}