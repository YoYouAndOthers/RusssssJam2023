using Cysharp.Threading.Tasks;

namespace RussSurvivor.Runtime.Gameplay.Battle.Enemies
{
  public interface IEnemyTypeProvider
  {
    UniTask InitializeAsync();
    bool TryGetEnemyConfig(EnemyType enemyTypeId, out EnemyConfig config);
  }
}