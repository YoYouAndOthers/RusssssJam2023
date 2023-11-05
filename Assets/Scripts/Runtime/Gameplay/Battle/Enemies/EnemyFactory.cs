using UnityEngine;
using Zenject;

namespace RussSurvivor.Runtime.Gameplay.Battle.Enemies
{
  public class EnemyFactory
  {
    private readonly IInstantiator _instantiator;
    private readonly IEnemyTypeProvider _enemyTypeProvider;
    private readonly EnemySpawner _enemySpawner;

    public EnemyFactory(IInstantiator instantiator, IEnemyTypeProvider enemyTypeProvider, EnemySpawner enemySpawner)
    {
      _instantiator = instantiator;
      _enemyTypeProvider = enemyTypeProvider;
      _enemySpawner = enemySpawner;
    }

    public EnemyBehaviour Create(EnemyType type)
    {
      EnemyConfig enemyConfig = _enemyTypeProvider.TryGetEnemyConfig(type, out EnemyConfig config)
        ? config
        : throw new System.Exception($"Enemy config for type {type} not found");

      var result = _instantiator.InstantiatePrefabForComponent<EnemyBehaviour>(
        enemyConfig.Prefab,
        _enemySpawner.GetRandomPosition(),
        Quaternion.identity,
        _enemySpawner.transform);

      result.Initialize(enemyConfig);
      return result;
    }
  }
}