using System.Collections.Generic;
using System.Linq;

namespace RussSurvivor.Runtime.Gameplay.Battle.Enemies
{
  public class EnemyRegistry : IEnemyRegistry
  {
    private readonly List<EnemyBehaviour> _allEnemies = new();
    private readonly Dictionary<EnemyType, List<EnemyBehaviour>> _enemiesByType = new();
    public bool AllEnemiesDead => _allEnemies.All(k => k == null);
    public bool NoBoss => true;

    public void Add(EnemyBehaviour enemyBehaviour, EnemyType enemyType)
    {
      _allEnemies.Add(enemyBehaviour);
      if (!_enemiesByType.ContainsKey(enemyType))
        _enemiesByType.Add(enemyType, new List<EnemyBehaviour> { enemyBehaviour });
      else
        _enemiesByType[enemyType].Add(enemyBehaviour);
    }

    public void Remove(EnemyBehaviour enemyBehaviour)
    {
      _allEnemies.Remove(enemyBehaviour);
      _enemiesByType[enemyBehaviour.EnemyType].Remove(enemyBehaviour);
    }

    public IEnumerable<EnemyBehaviour> GetEnemiesOfType(EnemyType key)
    {
      if (!_enemiesByType.ContainsKey(key))
        return Enumerable.Empty<EnemyBehaviour>();
      return _enemiesByType[key].Where(k => k != null);
    }
  }
}