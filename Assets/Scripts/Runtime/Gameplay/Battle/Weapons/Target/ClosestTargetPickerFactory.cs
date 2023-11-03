using UnityEngine;
using Zenject;

namespace RussSurvivor.Runtime.Gameplay.Battle.Weapons.Target
{
  public class ClosestTargetPickerFactory : PlaceholderFactory<Transform, ClosestTargetPicker>, IInitializable
  {
    private readonly IInstantiator _instantiator;
    private ClosestTargetPicker _prefab;

    public ClosestTargetPickerFactory(IInstantiator instantiator) =>
      _instantiator = instantiator;

    public void Initialize()
    {
      _prefab = Resources.Load<ClosestTargetPicker>("Prefabs/ClosestTargetPicker");
    }

    public override ClosestTargetPicker Create(Transform character)
    {
      return _instantiator.InstantiatePrefabForComponent<ClosestTargetPicker>(_prefab, character);
    }
  }
}