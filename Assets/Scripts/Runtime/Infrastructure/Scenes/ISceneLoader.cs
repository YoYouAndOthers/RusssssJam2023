using Cysharp.Threading.Tasks;
using RussSurvivor.Runtime.Infrastructure.Installers;
using UnityEngine.SceneManagement;

namespace RussSurvivor.Runtime.Infrastructure.Scenes
{
  public interface ISceneLoader
  {
    UniTask LoadSceneAsync(SceneEntrance.SceneName sceneName, LoadSceneMode loadSceneMode = LoadSceneMode.Single);
  }
}