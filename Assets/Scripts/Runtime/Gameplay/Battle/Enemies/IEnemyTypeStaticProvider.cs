using System;
using Cysharp.Threading.Tasks;

namespace RussSurvivor.Runtime.Gameplay.Battle.Enemies
{
  public interface IEnemyTypeStaticProvider
  {
    UniTask InitializeAsync();
    bool TryGetEnemyConfig(Guid enemyTypeId, out EnemyConfig config);
  }
}