using System.Linq;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Battle.Weapons.Target
{
  public class ClosestTargetDirectionPickStrategy : ITargetDirectionPickStrategy
  {
    private readonly ITarget _owner;
    private readonly ClosestTargetPicker _closestTargetPicker;

    public ClosestTargetDirectionPickStrategy(ITarget owner, ClosestTargetPicker closestTargetPicker)
    {
      _owner = owner;
      _closestTargetPicker = closestTargetPicker;
    }

    public bool Get(out Vector3 direction)
    {
      if (_closestTargetPicker.Targets.Any())
      {
        direction = _closestTargetPicker.Targets.OrderBy(k => Vector2.Distance(_owner.Position, k.Position)).First()
          .Position;
        return true;
      }

      direction = Vector3.zero;
      return false;
    }
  }
}