namespace RussSurvivor.Runtime.Infrastructure.Installers
{
  public static class SceneEntrance
  {
    public enum SceneName : byte
    {
      NotInitialized = 0,
      Bootstrap = 1,
      Battle = 2,
      Gameplay = 3,
      Town = 4,
      MainMenu = 5
    }

    public static SceneName InitializedScene = SceneName.NotInitialized;
  }
}