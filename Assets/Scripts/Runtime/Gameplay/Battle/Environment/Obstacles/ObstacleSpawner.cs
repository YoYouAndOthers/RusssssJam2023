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
        Vector3 randomOnRing;
        do
        {
          randomOnRing = (Vector3.down * 2).RandomOnRing(5, obstacleConfig.Radius);
        } while (randomOnRing.y >= 0);

        _instantiator.InstantiatePrefabForComponent<ObstacleBehaviour>(obstacleConfig.Prefab,
          randomOnRing, Quaternion.identity, null);
      }
    }
  }
}