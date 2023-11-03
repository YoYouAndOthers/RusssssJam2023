namespace RussSurvivor.Runtime.Gameplay.Common.Player
{
  public interface ITownPlayerRegistry : IPlayerRegistry
  {
    PlayerTownBehaviour GetTownPlayer();
  }
}