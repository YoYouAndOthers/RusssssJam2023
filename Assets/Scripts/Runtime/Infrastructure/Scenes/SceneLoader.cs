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
        { SceneEntrance.SceneName.Battle, "Battle" },
        { SceneEntrance.SceneName.Gameplay, "Gameplay" },
        { SceneEntrance.SceneName.Town, "Town" },
        { SceneEntrance.SceneName.MainMenu, "MainMenu" }
      };

    private ICurtain _curtain;

    public void Initialize(ICurtain curtain)
    {
      _curtain = curtain;
    }

    public void LoadScene(SceneEntrance.SceneName sceneName, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
    {
      Debug.Log($"Loading scene {sceneName.ToString()}");
      SceneManager.LoadScene(SceneNames[sceneName], loadSceneMode);
      Debug.Log($"Scene {SceneNames[sceneName]} loaded");
    }

    public async UniTask LoadSceneAsync(SceneEntrance.SceneName sceneName,
      LoadSceneMode loadSceneMode = LoadSceneMode.Single)
    {
      Debug.Log($"Loading scene {sceneName.ToString()}");
      await SceneManager.LoadSceneAsync(SceneNames[sceneName], loadSceneMode);
      Debug.Log($"Scene {SceneNames[sceneName]} loaded");
    }

    public async UniTask UnloadSceneAsync(SceneEntrance.SceneName sceneName)
    {
      Debug.Log($"Unloading scene {sceneName.ToString()}");
      await SceneManager.UnloadSceneAsync(SceneNames[sceneName]);
      Debug.Log($"Scene {SceneNames[sceneName]} unloaded");
    }
  }
}