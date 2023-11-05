using RussSurvivor.Runtime.Gameplay.Battle.Environment.Navigation;
using RussSurvivor.Runtime.Gameplay.Battle.Weapons.Damage;
using RussSurvivor.Runtime.Gameplay.Battle.Weapons.Target;
using UnityEngine;
using Zenject;

namespace RussSurvivor.Runtime.Gameplay.Battle.Environment.Obstacles
{
  public class ObstacleBehaviour : MonoBehaviour, ITarget, IDamagable
  {
    private INavMeshService _navMeshService;
    public Vector3 Position => transform.position;

    [Inject]
    private void Construct(INavMeshService navMeshService)
    {
      _navMeshService = navMeshService;
    }
    
    public bool TryTakeDamage(float damage, bool percent = false)
    {
      Kill();
      return true;
    }

    public void Kill()
    {
      Destroy(gameObject);
      _navMeshService.RebuildNavMesh();
    }
  }
}