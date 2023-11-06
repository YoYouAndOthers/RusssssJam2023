using System.Collections.Generic;
using System.Linq;
using RussSurvivor.Runtime.Gameplay.Battle.Weapons;
using RussSurvivor.Runtime.Gameplay.Battle.Weapons.Content;
using RussSurvivor.Runtime.Gameplay.Common.Timing;
using RussSurvivor.Runtime.Gameplay.Town.Economics.Currency;
using UniRx;
using UnityEngine;
using Zenject;

namespace RussSurvivor.Runtime.Gameplay.Town.Economics.Trade
{
  public class TraderService : ITraderService
  {
    private readonly ReactiveCollection<WeaponConfig> _weaponsForTrade = new();
    private readonly List<WeaponConfig> _cart = new();
    public IReadOnlyReactiveCollection<WeaponConfig> WeaponsForTrade => _weaponsForTrade;

    public BoolReactiveProperty IsTrading { get; } = new();

    public BoolReactiveProperty IsBeaten { get; } = new();
    private bool _isBeaten;

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

    public void AddToCart(WeaponConfig weapon)
    {
      _cart.Add(weapon);
    }

    public bool IsInCart(WeaponConfig weapon)
    {
      return _cart.Contains(weapon);
    }

    public void RemoveFromCart(WeaponConfig weapon)
    {
      _cart.Remove(weapon);
    }

    public IReadOnlyDictionary<CurrencyType, int> GetCartCost()
    {
      Dictionary<CurrencyType, int> result = _cart
        .GroupBy(k => k.CostType)
        .ToDictionary(k => k.Key, k => k.Sum(l => l.CostAmount));
      return Modified(result);
    }

    private IReadOnlyDictionary<CurrencyType, int> Modified(Dictionary<CurrencyType, int> oldValue)
    {
      if (IsBeaten.Value)
      {
        bool polushkiExist = oldValue.TryGetValue(CurrencyType.Polushka, out int polushka);
        bool zelkovyExists = oldValue.TryGetValue(CurrencyType.Zelkovyu, out int zelkovyu);
        bool chetvertushkaExists = oldValue.TryGetValue(CurrencyType.Chetvertushka, out int chetvertushka);
        bool serebryachokExists = oldValue.TryGetValue(CurrencyType.Serebryachok, out int serebryachok);

        var result = new Dictionary<CurrencyType, int>
        {
          [CurrencyType.Zelkovyu] = 0,
          [CurrencyType.Polushka] = polushkiExist ? polushka / 2 + (zelkovyExists ? zelkovyu : 0) : 0,
          [CurrencyType.Serebryachok] = 0,
          [CurrencyType.Chetvertushka] =
            serebryachokExists ? serebryachok / 2 + (chetvertushkaExists ? chetvertushka : 0) : 0
        };

        return result;
      }

      return oldValue;
    }
  }
}