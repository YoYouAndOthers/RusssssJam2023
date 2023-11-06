using System.Collections.Generic;
using RussSurvivor.Runtime.Gameplay.Battle.Weapons;
using RussSurvivor.Runtime.Gameplay.Battle.Weapons.Content;
using RussSurvivor.Runtime.Gameplay.Common.Timing;
using UniRx;
using UnityEngine;
using Zenject;

namespace RussSurvivor.Runtime.Gameplay.Town.Economics.Trade
{
  public class TraderService : ITraderService
  {
    private readonly ReactiveCollection<WeaponConfig> _weaponsForTrade = new();
    public IReadOnlyReactiveCollection<WeaponConfig> WeaponsForTrade => _weaponsForTrade;

    public BoolReactiveProperty IsTrading { get; } = new();

    private IPauseService _pauseService;
    private IWeaponConfigProvider _weaponConfigProvider;

    [Inject]
    private void Construct(IPauseService pauseService, IWeaponConfigProvider weaponConfigProvider)
    {
      _pauseService = pauseService;
      _weaponConfigProvider = weaponConfigProvider;
    }

    public void Initialize()
    {
      Debug.Log("Trader service initialized");
    }

    public void StartTrade()
    {
      Debug.Log("Trade started");
      _pauseService.Pause();
      IEnumerable<WeaponConfig> weaponsForTrade = _weaponConfigProvider.GetRandomWeaponTypesToSell(5);
      IsTrading.Value = true;
      foreach (WeaponConfig weaponConfig in weaponsForTrade)
        _weaponsForTrade.Add(weaponConfig);
    }

    public void EndTrade()
    {
      _weaponsForTrade.Clear();
      IsTrading.Value = false;
      Debug.Log("Trade ended");
      _pauseService.Resume();
    }
  }
}