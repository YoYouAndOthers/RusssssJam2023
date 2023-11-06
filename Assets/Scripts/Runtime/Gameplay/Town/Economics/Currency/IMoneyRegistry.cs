using UniRx;
using Zenject;

namespace RussSurvivor.Runtime.Gameplay.Town.Economics.Currency
{
  public interface IMoneyRegistry : IInitializable
  {
    IReadOnlyReactiveDictionary<CurrencyType, int> Money { get; }
    public void AddMoney(CurrencyType currencyType, int amount);
    public bool TrySpendMoney(CurrencyType currencyType, int amount);
    public bool CanSpendMoney(CurrencyType currencyType, int amount);
  }
}