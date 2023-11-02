using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using RussSurvivor.Runtime.Infrastructure.Installers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RussSurvivor.Runtime.Infrastructure.Scenes
{
  public class SceneLoader : ISceneLoader
  {
    private static readonly Dictionary<SceneEntrance.SceneName, string> SceneNames =
      new()
      {
        { SceneEntrance.SceneName.Bootstrap, "Bootstrap" },
        { SceneEntrance.SceneName.Gameplay, "Gameplay" }
      };

    public async UniTask LoadSceneAsync(SceneEntrance.SceneName sceneName,
      LoadSceneMode loadSceneMode = LoadSceneMode.Single)
    {
      Debug.Log($"Loading scene {sceneName.ToString()}");
      await SceneManager.LoadSceneAsync(SceneNames[sceneName], loadSceneMode);
      Debug.Log($"Scene {SceneNames[sceneName]} loaded");
    }
  }
}