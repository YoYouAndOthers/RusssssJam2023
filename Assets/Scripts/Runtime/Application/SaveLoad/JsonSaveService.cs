using RussSurvivor.Runtime.Application.Progress;
using RussSurvivor.Runtime.Infrastructure.Constants;
using Unity.Plastic.Newtonsoft.Json;

namespace RussSurvivor.Runtime.Application.SaveLoad
{
  public class JsonSaveService : ISaveService
  {
    private readonly IPersistentProgress _persistentProgress;

    public JsonSaveService(IPersistentProgress persistentProgress) =>
      _persistentProgress = persistentProgress;

    public void Save()
    {
      string path = System.IO.Path.Join(UnityEngine.Application.persistentDataPath, Files.SaveFile);
      using var file = new System.IO.StreamWriter(path, false);
      string json = JsonConvert.SerializeObject(_persistentProgress.Progress, Formatting.Indented);
      file.Write(json);
    }
  }
}