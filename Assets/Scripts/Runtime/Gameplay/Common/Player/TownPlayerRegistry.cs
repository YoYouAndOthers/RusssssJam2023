namespace RussSurvivor.Runtime.Gameplay.Common.Player
{
  public class TownPlayerRegistry : ITownPlayerRegistry
  {
    private PlayerTownBehaviour _playerTownBehaviour;
    
    public void RegisterPlayer(PlayerBehaviourBase player)
    {
      _playerTownBehaviour = player as PlayerTownBehaviour;
    }

    public PlayerBehaviourBase GetPlayer()
    {
      return _playerTownBehaviour;
    }

    public PlayerTownBehaviour GetTownPlayer()
    {
      return _playerTownBehaviour;
    }
  }
}