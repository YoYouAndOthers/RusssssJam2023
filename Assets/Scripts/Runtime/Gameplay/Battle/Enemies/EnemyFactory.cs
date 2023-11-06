using RussSurvivor.Runtime.Gameplay.Common.Player;
using UnityEngine;
using Zenject;

namespace RussSurvivor.Runtime.Gameplay.Battle.Enemies
{
  public class EnemyFactory
  {
    private readonly IInstantiator _instantiator;
    private readonly IEnemyTypeProvider _enemyTypeProvider;
    private readonly EnemySpawner _enemySpawner;
    private readonly IBattlePlayerRegistry _playerRegistry;

    public EnemyFactory(IInstantiator instantiator, IEnemyTypeProvider enemyTypeProvider,
      IBattlePlayerRegistry playerRegistry, EnemySpawner enemySpawner)
    {
      _playerRegistry = playerRegistry;
      _instantiator = instantiator;
      _enemyTypeProvider = enemyTypeProvider;
      _enemySpawner = enemySpawner;
    }

    public EnemyBehaviour Create(EnemyType type)
    {
      EnemyConfig enemyConfig = _enemyTypeProvider.TryGetEnemyConfig(type, out EnemyConfig config)
        ? config
        : throw new System.Exception($"Enemy config for type {type} not found");

      Vector3 position;
      do
      {
        position = _enemySpawner.GetRandomPosition();
      } while (Vector3.Distance(_playerRegistry.GetBattlePlayer().Position, position) < 5f);

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