using System.IO;
using RussSurvivor.Runtime.Application.Progress;
using RussSurvivor.Runtime.Application.Progress.Data;
using RussSurvivor.Runtime.Infrastructure.Constants;
using Unity.Plastic.Newtonsoft.Json;

namespace RussSurvivor.Runtime.Application.SaveLoad
{
  public class JsonLoadService : ILoadService
  {
    private IPersistentProgress _persistentProgress;

    public JsonLoadService(IPersistentProgress persistentProgress) =>
      _persistentProgress = persistentProgress;

    public void Load()
    {
      string savePath = Path.Join(UnityEngine.Application.persistentDataPath, Files.SaveFile);
      if(File.Exists(savePath))
      {
        string json = File.ReadAllText(savePath);
        _persistentProgress.Progress = JsonConvert.DeserializeObject<GameProgress>(json);
      }
      else
      {
        UnityEngine.Debug.LogWarning("No save file found");
        _persistentProgress.Progress = new GameProgress();
      }
    }
  }
}