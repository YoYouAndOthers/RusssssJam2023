using System.IO;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using RussSurvivor.Runtime.Application.Progress;
using RussSurvivor.Runtime.Application.Progress.Data;
using RussSurvivor.Runtime.Application.Progress.Watchers;
using RussSurvivor.Runtime.Infrastructure.Constants;
using UnityEngine;

namespace RussSurvivor.Runtime.Application.SaveLoad
{
  public class JsonLoadService : ILoadService
  {
    private readonly IPersistentProgress _persistentProgress;
    private readonly IProgressWatcherService _progressWatcherService;

    public JsonLoadService(IPersistentProgress persistentProgress, IProgressWatcherService progressWatcherService)
    {
      _persistentProgress = persistentProgress;
      _progressWatcherService = progressWatcherService;
    }

    public bool HasSave()
    {
      return File.Exists(GetSavePath());
    }

    public void Load()
    {
      if (HasSave())
      {
        string json = File.ReadAllText(GetSavePath());
        _persistentProgress.Progress = JsonConvert.DeserializeObject<GameProgress>(json);
      }
      else
      {
        Debug.LogWarning("No save file found");
        _persistentProgress.Progress = new GameProgress();
      }
    }

    public async UniTask LoadAsync()
    {
      if (File.Exists(GetSavePath()))
      {
        string json = await File.ReadAllTextAsync(GetSavePath());
        _persistentProgress.Progress = JsonConvert.DeserializeObject<GameProgress>(json);
        Debug.Log("Loaded save file");
      }
      else
      {
        Debug.LogWarning("No save file found");
        _persistentProgress.Progress = new GameProgress();
      }

      foreach (IProgressReader progressReader in _progressWatcherService.ProgressReaders)
        progressReader.Load(_persistentProgress.Progress);
    }

    private static string GetSavePath()
    {
      return Path.Join(UnityEngine.Application.persistentDataPath, Files.SaveFile);
    }
  }
}