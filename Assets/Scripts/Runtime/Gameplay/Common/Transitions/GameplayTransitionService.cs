using RussSurvivor.Runtime.Gameplay.Common.Cinema;
using RussSurvivor.Runtime.Infrastructure.Installers;
using RussSurvivor.Runtime.Infrastructure.Scenes;
using UnityEngine.SceneManagement;

namespace RussSurvivor.Runtime.Gameplay.Common.Transitions
{
  public class GameplayTransitionService : IGameplayTransitionService
  {
    private readonly ISceneLoader _sceneLoader;
    private readonly ICurtain _curtain;
    private CameraFollower _cameraFollower;

    public GameplayTransitionService(ISceneLoader sceneLoader, ICurtain curtain, CameraFollower cameraFollower)
    {
      _sceneLoader = sceneLoader;
      _curtain = curtain;
      _cameraFollower = cameraFollower;
    }

    public async void GoToBattle()
    {
      _curtain.Show();
      _cameraFollower.Disable();
      await _sceneLoader.UnloadSceneAsync(SceneEntrance.SceneName.Town);
      await _sceneLoader.LoadSceneAsync(SceneEntrance.SceneName.Gameplay);
      await _sceneLoader.LoadSceneAsync(SceneEntrance.SceneName.Battle, LoadSceneMode.Additive);
    }

    public void ReturnToTown()
    {
    }
  }
}