using RussSurvivor.Runtime.Gameplay.Battle.Environment.Navigation;
using RussSurvivor.Runtime.Infrastructure.Extensions;
using UnityEngine;
using Zenject;

namespace RussSurvivor.Runtime.Gameplay.Battle.Environment.Obstacles
{
  public class ObstacleSpawner
  {
    private IInstantiator _instantiator;
    private INavMeshService _navMeshService;

    [Inject]
    private void Construct(IInstantiator instantiator, INavMeshService navMeshService)
    {
      _instantiator = instantiator;
      _navMeshService = navMeshService;
    }

    public void SpawnObstacles()
    {
      var obstacleConfig = Resources.Load<ObstaclesConfig>("Configs/Obstacles");
      Transform obstacleParent = new GameObject("Obstacles").transform;
      for (var i = 0; i < obstacleConfig.Number; i++)
      {
        Vector3 randomOnRing;
        do
        {
          randomOnRing = (Vector3.down * 2).RandomOnRing(5, obstacleConfig.Radius);
        } while (randomOnRing.y >= 0);

        _instantiator.InstantiatePrefabForComponent<ObstacleBehaviour>(obstacleConfig.Prefab,
          randomOnRing, Quaternion.identity, obstacleParent);
      }

      _navMeshService.RebuildNavMesh();
    }
  }
}