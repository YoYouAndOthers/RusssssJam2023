using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Common.Player
{
  public class BattlePlayerRegistry : IBattlePlayerRegistry
  {
    private PlayerBattleBehaviour _playerBattleBehaviour;

    public void RegisterPlayer(PlayerBehaviourBase player)
    {
      _playerBattleBehaviour = player as PlayerBattleBehaviour;
      Debug.Log(_playerBattleBehaviour);
    }

    public PlayerBehaviourBase GetPlayer()
    {
      return _playerBattleBehaviour;
    }

    public PlayerBattleBehaviour GetBattlePlayer()
    {
      return _playerBattleBehaviour;
    }
  }
}