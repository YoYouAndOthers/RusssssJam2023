using System.Collections.Generic;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Battle.Weapons.Target
{
  [RequireComponent(typeof(CircleCollider2D))]
  public class ClosestTargetPicker : MonoBehaviour
  {
    private readonly List<ITarget> _targets = new();

    public IEnumerable<ITarget> Targets => _targets;

    private CircleCollider2D _collider;
    private ITarget _owner;

    public float Radius
    {
      get => _collider.radius;
      set => _collider.radius = value;
    }

    public void Initialize(ITarget owner, float radius, int layerMask)
    {
      _collider = GetComponent<CircleCollider2D>();
      _collider.isTrigger = true;
      _collider.callbackLayers = new LayerMask { value = layerMask };
      Radius = radius;
      _owner = owner;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      if (other.TryGetComponent(out ITarget target) && target != _owner)
      {
        Debug.Log($"Add target {target}");
        _targets.Add(target);
      }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
      if (other.TryGetComponent(out ITarget target) && target != _owner)
      {
        _targets.Remove(target);
      }
    }
  }
}