using RussSurvivor.Runtime.Infrastructure.Scenes;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RussSurvivor.Runtime.Application
{
  public class ApplicationService : IApplicationService
  {
    private ISceneLoader _sceneLoader;

    public ApplicationService(ISceneLoader sceneLoader) =>
      _sceneLoader = sceneLoader;

    public void LoadGame()
    {
    }

    public void NewGame()
    {
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