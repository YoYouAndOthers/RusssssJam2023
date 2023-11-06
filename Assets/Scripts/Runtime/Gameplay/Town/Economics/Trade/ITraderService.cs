using RussSurvivor.Runtime.Gameplay.Battle.Weapons;
using UniRx;
using Zenject;

namespace RussSurvivor.Runtime.Gameplay.Town.Economics.Trade
{
  public interface ITraderService : IInitializable
  {
    IReadOnlyReactiveCollection<WeaponConfig> WeaponsForTrade { get; }
    BoolReactiveProperty IsTrading { get; }
    void StartTrade();
    void EndTrade();
    void AddToCart(WeaponConfig weapon);
  }
}