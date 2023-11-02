using RussSurvivor.Runtime.Gameplay.Common.Player;

namespace RussSurvivor.Runtime.Gameplay.Battle.Characters
{
  public class CharacterRegistry : ICharacterRegistry
  {
    private PlayerBehaviour _player;

    public PlayerBehaviour GetPlayer()
    {
      return _player;
    }

    public void RegisterPlayer(PlayerBehaviour player)
    {
      _player = player;
    }
  }
}