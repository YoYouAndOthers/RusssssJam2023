using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Common.Player
{
  public class PlayerPrefabProvider : IPlayerPrefabProvider
  {
    private Dictionary<PlayerPrefabType, PlayerBehaviourBase> _prefabsByType;

    public async UniTask Initialize()
    {
      Debug.Log("PlayerPrefabProvider initialization started");
      if (_prefabsByType != null)
      {
        Debug.Log("PlayerPrefabProvider already initialized");
        return;
      }

      var config = await Resources.LoadAsync<PlayerConfig>("Configs/PlayerConfig") as PlayerConfig;
      Debug.Assert(config != null, "PlayerConfig not found!");
      _prefabsByType = new Dictionary<PlayerPrefabType, PlayerBehaviourBase>
      {
        [PlayerPrefabType.Town] = config.TownPrefab,
        [PlayerPrefabType.Battle] = config.BattlePrefab
      };
      Debug.Log("PlayerPrefabProvider initialization finished");
    }

    public PlayerBehaviourBase GetPlayerPrefab(PlayerPrefabType type)
    {
      return _prefabsByType[type];
    }
  }
}