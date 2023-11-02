namespace RussSurvivor.Runtime.Infrastructure.Installers
{
  public static class SceneEntrance
  {
    public enum SceneName
    {
      NotInitialized = 0,
      Bootstrap = 1,
      Gameplay = 2
    }

    public static SceneName InitializedScene = SceneName.NotInitialized;
  }
}