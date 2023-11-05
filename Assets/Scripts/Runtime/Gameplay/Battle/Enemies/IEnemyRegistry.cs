using System.Collections.Generic;

namespace RussSurvivor.Runtime.Gameplay.Battle.Enemies
{
  public interface IEnemyRegistry
  {
    bool AllEnemiesDead { get; }
    bool NoBoss { get; }
    void Add(EnemyBehaviour enemyBehaviour, EnemyType enemyType);
    void Remove(EnemyBehaviour enemyBehaviour);
    IEnumerable<EnemyBehaviour> GetEnemiesOfType(EnemyType key);
  }
}