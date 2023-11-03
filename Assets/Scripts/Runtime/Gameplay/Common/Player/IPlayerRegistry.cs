namespace RussSurvivor.Runtime.Gameplay.Common.Player
{
  public interface IPlayerRegistry
  {
    void RegisterPlayer(PlayerBehaviourBase player);
    PlayerBehaviourBase GetPlayer();
  }
}