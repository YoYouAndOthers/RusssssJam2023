using RussSurvivor.Runtime.Application.Progress.Data;

namespace RussSurvivor.Runtime.Application.Progress
{
  public class PersistentProgress : IPersistentProgress
  {
    public GameProgress Progress { get; set; }
  }
}