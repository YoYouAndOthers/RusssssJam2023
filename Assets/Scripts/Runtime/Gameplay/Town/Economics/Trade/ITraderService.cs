using System.Collections.Generic;
using RussSurvivor.Runtime.Gameplay.Battle.Weapons;
using RussSurvivor.Runtime.Gameplay.Town.Economics.Currency;
using UniRx;
using Zenject;

namespace RussSurvivor.Runtime.Gameplay.Town.Economics.Trade
{
  public interface ITraderService : IInitializable
  {
    IReadOnlyReactiveCollection<WeaponConfig> WeaponsForTrade { get; }
    BoolReactiveProperty IsTrading { get; }
    BoolReactiveProperty IsBeaten { get; }
    void StartTrade();
    void EndTrade();
    void AddToCart(WeaponConfig weapon);
    bool IsInCart(WeaponConfig weapon);
    void RemoveFromCart(WeaponConfig weapon);
    IReadOnlyDictionary<CurrencyType, int> GetCartCost();
  }
}