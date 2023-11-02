using RussSurvivor.Runtime.Infrastructure.Extensions;
using UnityEngine;
using Zenject;

namespace RussSurvivor.Runtime.Gameplay.Battle.Environment.Obstacles
{
  public class ObstacleSpawner
  {
    private IInstantiator _instantiator;

    [Inject]
    private void Construct(IInstantiator instantiator)
    {
      _instantiator = instantiator;
    }
    
    public void SpawnObstacles()
    {
      var obstacleConfig = Resources.Load<ObstaclesConfig>("Configs/Obstacles");
      for (var i = 0; i < obstacleConfig.Number; i++)
      {
        _instantiator.InstantiatePrefabForComponent<ObstacleBehaviour>(obstacleConfig.Prefab, Vector3.zero.RandomOnRing(5, obstacleConfig.Radius), Quaternion.identity, null);
      }
    }
  }
}