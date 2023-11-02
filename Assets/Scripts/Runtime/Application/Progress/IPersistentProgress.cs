using RussSurvivor.Runtime.Application.Progress.Data;

namespace RussSurvivor.Runtime.Application.Progress
{
  public interface IPersistentProgress
  {
    GameProgress Progress { get; set; }
  }
}