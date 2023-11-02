using System.Collections.Generic;

namespace RussSurvivor.Runtime.Application.Progress.Watchers
{
  public interface IProgressWatcherService
  {
    IEnumerable<IProgressReader> ProgressReaders { get; }
    IEnumerable<IProgressWriter> ProgressWriters { get; }
    void Register(IProgressReader progressWatcher);
  }
}