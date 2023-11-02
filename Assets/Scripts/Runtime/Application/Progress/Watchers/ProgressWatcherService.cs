using System.Collections.Generic;

namespace RussSurvivor.Runtime.Application.Progress.Watchers
{
  public class ProgressWatcherService : IProgressWatcherService
  {
    private readonly HashSet<IProgressReader> _progressReaders = new();
    private readonly HashSet<IProgressWriter> _progressWriters = new();

    public IEnumerable<IProgressReader> ProgressReaders => _progressReaders;
    public IEnumerable<IProgressWriter> ProgressWriters => _progressWriters;

    public void Register(IProgressReader progressWatcher)
    {
      if (progressWatcher is IProgressWriter progressWriter)
        _progressWriters.Add(progressWriter);
      _progressReaders.Add(progressWatcher);
    }
  }
}