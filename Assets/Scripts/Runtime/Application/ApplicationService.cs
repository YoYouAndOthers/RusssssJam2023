using RussSurvivor.Runtime.Infrastructure.Installers;
using RussSurvivor.Runtime.Infrastructure.Scenes;
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

    public void NewGame()
    {
      _curtain.Show();
      _sceneLoader.LoadScene(SceneEntrance.SceneName.Gameplay);
    }

    public void Quit()
    {
#if UNITY_EDITOR
      EditorApplication.isPlaying = false;
#else
      UnityEngine.Application.Quit();
#endif
    }
  }
}