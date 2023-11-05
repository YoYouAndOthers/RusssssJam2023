using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Common.Timing
{
  public class CooldownService : ICooldownService, IPauseService
  {
    private int _counter;
    private IDayTimer _dayTimer;

    private float _deltaTime;
    private HashSet<ICooldownUpdatable> _updatables = new();

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

    public BoolReactiveProperty IsPaused { get; } = new();

    public void Pause()
    {
      IsPaused.Value = true;
    }

    public void Resume()
    {
      IsPaused.Value = false;
    }

    public void PerformTick(float deltaTime)
    {
      if(IsPaused.Value)
        return;
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
        if (updatable == null || updatable.IsReady)
          continue;
        updatable.UpdateCooldown(deltaTime);
      }
    }
  }
}