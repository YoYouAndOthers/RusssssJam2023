namespace RussSurvivor.Runtime.Gameplay.Common.Player
{
  public interface IBattlePlayerRegistry : IPlayerRegistry
  {
    PlayerBattleBehaviour GetBattlePlayer();
  }
}