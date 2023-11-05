using RussSurvivor.Runtime.Infrastructure.Installers;

namespace RussSurvivor.Runtime.Gameplay.Common.Transitions
{
  public interface IGameplayTransitionService
  {
    SceneEntrance.SceneName CurrentScene { get; set; }
    void GoThroughGates();
  }
}