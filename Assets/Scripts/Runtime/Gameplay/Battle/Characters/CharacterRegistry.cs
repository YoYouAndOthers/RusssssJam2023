using RussSurvivor.Runtime.Gameplay.Common.Player;

namespace RussSurvivor.Runtime.Gameplay.Battle.Characters
{
  public class CharacterRegistry : ICharacterRegistry
  {
    private PlayerBattleBehaviour _playerBattle;

    public PlayerBattleBehaviour GetPlayer()
    {
      return _playerBattle;
    }

    public void RegisterPlayer(PlayerBattleBehaviour playerBattle)
    {
      _playerBattle = playerBattle;
    }
  }
}