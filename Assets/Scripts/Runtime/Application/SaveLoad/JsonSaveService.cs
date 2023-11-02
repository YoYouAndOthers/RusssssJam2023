using RussSurvivor.Runtime.Application.Progress;
using RussSurvivor.Runtime.Application.Progress.Watchers;
using RussSurvivor.Runtime.Infrastructure.Constants;
using Unity.Plastic.Newtonsoft.Json;

namespace RussSurvivor.Runtime.Application.SaveLoad
{
  public class JsonSaveService : ISaveService
  {
    private readonly IPersistentProgress _persistentProgress;
    private readonly IProgressWatcherService _progressWatcherService;

    public JsonSaveService(IPersistentProgress persistentProgress, IProgressWatcherService progressWatcherService)
    {
      _progressWatcherService = progressWatcherService;
      _persistentProgress = persistentProgress;
    }

    public void Save()
    {
      foreach (IProgressWriter progressWriter in _progressWatcherService.ProgressWriters)
        progressWriter.Save(_persistentProgress.Progress);

      string path = System.IO.Path.Join(UnityEngine.Application.persistentDataPath, Files.SaveFile);
      using var file = new System.IO.StreamWriter(path, false);
      string json = JsonConvert.SerializeObject(_persistentProgress.Progress, Formatting.Indented);
      file.Write(json);
    }
  }
}