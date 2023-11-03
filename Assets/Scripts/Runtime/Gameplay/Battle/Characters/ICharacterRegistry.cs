using RussSurvivor.Runtime.Gameplay.Common.Player;

namespace RussSurvivor.Runtime.Gameplay.Battle.Characters
{
  public interface ICharacterRegistry
  {
    PlayerBehaviour GetPlayer();
    void RegisterPlayer(PlayerBehaviour player);
  }
}