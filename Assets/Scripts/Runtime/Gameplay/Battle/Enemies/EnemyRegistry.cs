using System.Collections.Generic;
using System.Linq;

namespace RussSurvivor.Runtime.Gameplay.Battle.Enemies
{
  public class EnemyRegistry : IEnemyRegistry
  {
    private readonly List<EnemyBehaviour> _enemies = new();
    public bool AllEnemiesDead => _enemies.All(k => k == null);
    public bool NoBoss => true;

    public void Add(EnemyBehaviour enemyBehaviour)
    {
      _enemies.Add(enemyBehaviour);
    }

    public void Remove(EnemyBehaviour enemyBehaviour)
    {
      _enemies.Remove(enemyBehaviour);
    }
  }
}