using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace RussSurvivor.Runtime.Gameplay.Battle.Enemies
{
  public class EnemyTypeStaticProvider : IEnemyTypeStaticProvider
  {
    private Dictionary<Guid, EnemyConfig> _enemyTypeById;

    public async UniTask InitializeAsync()
    {
      IList<EnemyConfig> enemyConfigs = 
        await Addressables.LoadAssetsAsync<EnemyConfig>(new List<string>(){ "Enemies" }, null, Addressables.MergeMode.Intersection);
      _enemyTypeById = enemyConfigs.ToDictionary(config => config.Id);
    }

    public bool TryGetEnemyConfig(Guid enemyTypeId, out EnemyConfig config)
    {
      return _enemyTypeById.TryGetValue(enemyTypeId, out config);
    }
  }
}