using System;
using System.Collections.Generic;
using System.Linq;
using RussSurvivor.Runtime.Gameplay.Battle.Weapons.Content;
using RussSurvivor.Runtime.Gameplay.Battle.Weapons.Registry;
using RussSurvivor.Runtime.Gameplay.Common.Player;
using RussSurvivor.Runtime.Gameplay.Common.Timing;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace RussSurvivor.Runtime.Gameplay.Battle.Weapons
{
  public class PlayerWeaponService : IPlayerWeaponService
  {
    private readonly List<WeaponBehaviourBase> _weapons = new();

    public bool IsReady => false;
    public float TimeLeft { get; }
    private IBattlePlayerRegistry _battlePlayerRegistry;
    private ICooldownService _cooldownService;
    private WeaponBehaviourBase _fists;
    private IWeaponConfigProvider _weaponConfigProvider;
    private WeaponFactory _weaponFactory;
    private IWeaponRegistry _weaponRegistry;

    [Inject]
    private void Construct(
      WeaponFactory weaponFactory,
      IWeaponRegistry weaponRegistry,
      IWeaponConfigProvider weaponConfigProvider,
      ICooldownService cooldownService,
      IBattlePlayerRegistry battlePlayerRegistry)
    {
      _weaponFactory = weaponFactory;
      _weaponRegistry = weaponRegistry;
      _weaponConfigProvider = weaponConfigProvider;
      _cooldownService = cooldownService;
      _battlePlayerRegistry = battlePlayerRegistry;
    }

    public void UpdateCooldown(float deltaTime)
    {
      IEnumerable<WeaponBehaviourBase> availableWeapons = GetAvailableWeapons();
      var weaponsCountPerTick = 2;
      foreach (WeaponBehaviourBase weapon in availableWeapons)
        if (weaponsCountPerTick > 0 && weapon.ReadyToPerform(out Vector3 direction))
        {
          weapon.Perform(direction);
          weaponsCountPerTick--;
        }

      if (weaponsCountPerTick > 0)
        if (_fists.ReadyToPerform(out Vector3 direction))
          _fists.Perform(direction);
    }

    public void Initialize()
    {
      Debug.Log("Player Weapons Service initialized");
      PlayerBattleBehaviour player = _battlePlayerRegistry.GetBattlePlayer();

      _fists = _weaponFactory.Create(player.Fists, player);
      _cooldownService.RegisterUpdatable(_fists);

      foreach (Guid id in _weaponRegistry.GetWeaponIds())
      {
        if (!_weaponConfigProvider.TryGetWeaponConfig(id, out WeaponConfig config))
          continue;
        WeaponBehaviourBase weapon = _weaponFactory.Create(config, player);
        _weapons.Add(weapon);
        _cooldownService.RegisterUpdatable(weapon);
      }
    }

    public void Dispose()
    {
      ClearWeapons();
      _cooldownService.UnregisterUpdatable(this);
    }

    public void ClearWeapons()
    {
      _cooldownService.UnregisterUpdatable(_fists);
      Object.DestroyImmediate(_fists);
      foreach (WeaponBehaviourBase weapon in _weapons)
      {
        _cooldownService.UnregisterUpdatable(weapon);
        Object.DestroyImmediate(weapon);
      }
    }

    private IEnumerable<WeaponBehaviourBase> GetAvailableWeapons()
    {
      return _weapons.Where(weapon => weapon.IsReady);
    }
  }
}