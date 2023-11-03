using RussSurvivor.Runtime.Infrastructure.Scenes;
using UnityEngine;
using Zenject;

namespace RussSurvivor.Runtime.Infrastructure.Installers
{
  public class BootstrapInstaller : MonoInstaller, IInitializable
  {
    private ICurtain _curtain;
    private ISceneLoader _sceneLoader;

    [Inject]
    private void Construct(ISceneLoader sceneLoader, ICurtain curtain)
    {
      _sceneLoader = sceneLoader;
      _curtain = curtain;
    }

    public void Initialize()
    {
      _curtain.Show();
      UnityEngine.Application.targetFrameRate = 60;
      Debug.Log("Bootstrap scene initializing");
      SceneEntrance.InitializedScene = SceneEntrance.SceneName.Bootstrap;
      _sceneLoader.LoadSceneAsync(SceneEntrance.SceneName.MainMenu);
    }

    public override void InstallBindings()
    {
      Container
        .BindInterfacesTo<BootstrapInstaller>()
        .FromInstance(this)
        .AsSingle();
    }
  }
}