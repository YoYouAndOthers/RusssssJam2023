using RussSurvivor.Runtime.Application.Progress.Data;

namespace RussSurvivor.Runtime.Application.Progress.Watchers
{
  public interface IProgressReader
  {
    void Load(GameProgress progress);
  }
}