using NavMeshPlus.Components;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Battle.Environment.Navigation
{
  public class NavMeshService : MonoBehaviour, INavMeshService
  {
    [SerializeField] private NavMeshSurface _navMeshSurface;

    public void RebuildNavMesh()
    {
      Physics2D.SyncTransforms();
      _navMeshSurface.BuildNavMesh();
    }
  }
}