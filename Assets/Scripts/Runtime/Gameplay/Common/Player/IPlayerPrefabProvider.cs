using Cysharp.Threading.Tasks;

namespace RussSurvivor.Runtime.Gameplay.Common.Player
{
  public interface IPlayerPrefabProvider
  {
    UniTask InitializeAsync();
    PlayerBehaviourBase GetPlayerPrefab(PlayerPrefabType type);
  }
}