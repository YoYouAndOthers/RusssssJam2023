using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Common.Player
{
  [CreateAssetMenu(fileName = "PlayerConfig", menuName = "RussSurvivor/Gameplay/PlayerConfig")]
  public class PlayerConfig : ScriptableObject
  {
    public PlayerBehaviourBase BattlePrefab;
    public PlayerBehaviourBase TownPrefab;
  }
}