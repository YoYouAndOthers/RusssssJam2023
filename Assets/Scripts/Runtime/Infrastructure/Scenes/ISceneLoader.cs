using Cysharp.Threading.Tasks;
using RussSurvivor.Runtime.Infrastructure.Installers;
using UnityEngine.SceneManagement;

namespace RussSurvivor.Runtime.Infrastructure.Scenes
{
  public interface ISceneLoader
  {
    void Initialize(ICurtain curtain);
    void LoadScene(SceneEntrance.SceneName sceneName, LoadSceneMode loadSceneMode = LoadSceneMode.Single);
    UniTask LoadSceneAsync(SceneEntrance.SceneName sceneName, LoadSceneMode loadSceneMode = LoadSceneMode.Single);
    UniTask UnloadSceneAsync(SceneEntrance.SceneName sceneName);
  }
}