using RussSurvivor.Runtime.Application.Progress.Data;

namespace RussSurvivor.Runtime.Application.Progress.Watchers
{
  public interface IProgressWriter : IProgressReader
  {
    void Save(GameProgress progress);
  }
}