using Cysharp.Threading.Tasks;
using UnityEngine;

namespace RussSurvivor.Runtime.UI.Gameplay.Battle
{
  public interface IDamageCountService
  {
    UniTask InitializeAsync();
    void ShowDamageCount(Vector3 position, int damage);
  }
}