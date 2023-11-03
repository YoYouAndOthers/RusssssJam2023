using Cysharp.Threading.Tasks;

namespace RussSurvivor.Runtime.Gameplay.Common.Player
{
  public interface IPlayerPrefabProvider
  {
    UniTask Initialize();
    PlayerBehaviourBase GetPlayerPrefab(PlayerPrefabType type);
  }
}