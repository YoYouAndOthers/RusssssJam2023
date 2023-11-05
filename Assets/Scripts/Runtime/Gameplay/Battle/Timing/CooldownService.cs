using System.Collections.Generic;
using System.Linq;
using RussSurvivor.Runtime.Gameplay.Common.Timing;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Battle.Timing
{
  public class CooldownService : ICooldownService
  {
    private HashSet<ICooldownUpdatable> _updatables = new();
    private int _counter;

    private float _deltaTime;
    private IDayTimer _dayTimer;

    public void RegisterUpdatable(ICooldownUpdatable updatable)
    {
      Debug.Log($"Registering updatable: {updatable}");
      _updatables.Add(updatable);
    }

    public void UnregisterUpdatable(ICooldownUpdatable updatable)
    {
      Debug.Log($"Unregistering updatable: {updatable}");
      _updatables.Remove(updatable);
    }

    public void PerformTick(float deltaTime)
    {
      _deltaTime += deltaTime;
      if (_counter % 3 == 0)
      {
        UpdateAll(_deltaTime);
        _deltaTime = 0;
      }
      else
      {
        _counter++;
      }

      if (_counter % 20 == 0)
      {
        _updatables = new HashSet<ICooldownUpdatable>(_updatables.ToArray());
        _counter = 1;
      }
    }

    private void UpdateAll(float deltaTime)
    {
      foreach (ICooldownUpdatable updatable in _updatables)
      {
        if(updatable == null || updatable.IsReady)
          continue;
        updatable.UpdateCooldown(deltaTime);
      }
    }
  }
}