using RussSurvivor.Runtime.Gameplay.Common.Player;

namespace RussSurvivor.Runtime.Gameplay.Battle.Characters
{
  public interface ICharacterRegistry
  {
    PlayerBattleBehaviour GetPlayer();
    void RegisterPlayer(PlayerBattleBehaviour playerBattle);
  }
}