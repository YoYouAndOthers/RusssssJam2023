using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Common.Player
{
  [CreateAssetMenu(fileName = "PlayerConfig", menuName = "RussSurvivor/Gameplay/PlayerConfig")]
  public class PlayerConfig : ScriptableObject
  {
    public PlayerBattleBehaviour BattlePrefab;
    public PlayerBattleBehaviour BasePrefab;
  }
}