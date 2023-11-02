using System.IO;
using Cysharp.Threading.Tasks;
using RussSurvivor.Runtime.Application.Progress;
using RussSurvivor.Runtime.Application.Progress.Data;
using RussSurvivor.Runtime.Infrastructure.Constants;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace RussSurvivor.Runtime.Application.SaveLoad
{
  public class JsonLoadService : ILoadService
  {
    private readonly IPersistentProgress _persistentProgress;

    public JsonLoadService(IPersistentProgress persistentProgress) =>
      _persistentProgress = persistentProgress;

    public void Load()
    {
      if (File.Exists(GetSavePath()))
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
    }

    private static string GetSavePath()
    {
      return Path.Join(UnityEngine.Application.persistentDataPath, Files.SaveFile);
    }
  }
}