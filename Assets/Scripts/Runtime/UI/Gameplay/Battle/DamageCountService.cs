using Cysharp.Threading.Tasks;
using DamageNumbersPro;
using UnityEngine;

namespace RussSurvivor.Runtime.UI.Gameplay.Battle
{
  public class DamageCountService : IDamageCountService
  {
    private DamageNumberMesh _prefab;

    public async UniTask InitializeAsync()
    {
      _prefab = await Resources.LoadAsync<DamageNumberMesh>("Prefabs/UI/Popups/Damage Number") as DamageNumberMesh;
    }

    public void ShowDamageCount(Vector3 position, int damage)
    {
      _prefab.Spawn(position);
    }
  }
}