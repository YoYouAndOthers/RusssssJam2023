using RussSurvivor.Runtime.Infrastructure.Installers;
using RussSurvivor.Runtime.Infrastructure.Scenes;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RussSurvivor.Runtime.Application
{
  public class ApplicationService : IApplicationService
  {
    private readonly ICurtain _curtain;
    private readonly ISceneLoader _sceneLoader;

    public ApplicationService(ISceneLoader sceneLoader, ICurtain curtain)
    {
      _sceneLoader = sceneLoader;
      _curtain = curtain;
    }

    public void LoadGame()
    {
    }

    public async void NewGame()
    {
      _curtain.Show();
      await _sceneLoader.LoadSceneAsync(SceneEntrance.SceneName.Gameplay);
      await _sceneLoader.LoadSceneAsync(SceneEntrance.SceneName.Town, LoadSceneMode.Additive);
    }

    public void Quit()
    {
#if UNITY_EDITOR
      EditorApplication.isPlaying = false;
#else
      Application.Quit();
#endif
    }
  }
}