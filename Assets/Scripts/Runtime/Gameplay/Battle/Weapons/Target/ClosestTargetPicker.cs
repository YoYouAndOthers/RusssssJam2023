using System.Collections.Generic;
using System.Globalization;
using RussSurvivor.Runtime.Gameplay.Battle.Enemies;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Battle.Weapons.Target
{
  [RequireComponent(typeof(CircleCollider2D))]
  public class ClosestTargetPicker : MonoBehaviour
  {
    private readonly List<ITarget> _targets = new();

    public IEnumerable<ITarget> Targets
    {
      get
      {
        _targets.RemoveAll(k => k == null);
        return _targets;
      }
    }

    public float Radius
    {
      get => _collider.radius;
      set => _collider.radius = value;
    }

    private CircleCollider2D _collider;
    private ITarget _owner;

    private void OnTriggerEnter2D(Collider2D other)
    {
      if (other.attachedRigidbody.TryGetComponent(out ITarget target) && target != _owner)
        _targets.Add(target);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
      if (other.attachedRigidbody.TryGetComponent(out ITarget target) && target != _owner)
        _targets.Remove(target);
    }

    public void Initialize(ITarget owner, float radius, int layerMask)
    {
      Debug.Assert(owner != null, "Owner is null");
      Debug.Log(
        $"ClosestTargetPicker for {owner} initialized with radius {radius.ToString(CultureInfo.InvariantCulture)}");
      _collider = GetComponent<CircleCollider2D>();
      _collider.isTrigger = true;
      _collider.callbackLayers = new LayerMask { value = layerMask };
      Radius = radius;
      _owner = owner;
    }

    public void RemoveTarget(EnemyBehaviour enemyBehaviour)
    {
      _targets.Remove(enemyBehaviour);
    }
  }
}