using UniRx;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Town.Economics.Currency
{
  public class MoneyRegistry : IMoneyRegistry
  {
    private readonly ReactiveDictionary<CurrencyType, int> _money = new();

    public IReadOnlyReactiveDictionary<CurrencyType, int> Money => _money;

    public void Initialize()
    {
      Debug.Log("Money registry initialized");
      _money.Add(CurrencyType.Polushka, 2);
      _money.Add(CurrencyType.Chetvertushka, 100);
    }

    public void AddMoney(CurrencyType currencyType, int amount)
    {
      if (_money.TryGetValue(currencyType, out int currentAmount))
        _money[currencyType] = currentAmount + amount;
      else
        _money.Add(currencyType, amount);
    }

    public bool TrySpendMoney(CurrencyType currencyType, int amount)
    {
      if (!CanSpendMoney(currencyType, amount))
        return false;
      _money[currencyType] -= amount;
      return true;
    }

    public bool CanSpendMoney(CurrencyType currencyType, int amount)
    {
      int allMoney = GetAllMoney();
      int amountInChetvertushka = MoneyConverter.ConvertToChetvertushka(amount, currencyType);

      return allMoney >= amountInChetvertushka;
    }

    private int GetAllMoney()
    {
      if (!_money.TryGetValue(CurrencyType.Polushka, out int amountPolushka))
      {
        amountPolushka = 0;
      }

      if (!_money.TryGetValue(CurrencyType.Serebryachok, out int amountSerebryachok))
      {
        amountSerebryachok = 0;
      }

      if (!_money.TryGetValue(CurrencyType.Zelkovyu, out int amountZelkovyu))
      {
        amountZelkovyu = 0;
      }

      if (!_money.TryGetValue(CurrencyType.Chetvertushka, out int amountChetvertushka))
      {
        amountChetvertushka = 0;
      }

      return amountChetvertushka +
             MoneyConverter.ConvertToChetvertushka(amountPolushka, CurrencyType.Polushka) +
             MoneyConverter.ConvertToChetvertushka(amountSerebryachok, CurrencyType.Serebryachok) +
             MoneyConverter.ConvertToChetvertushka(amountZelkovyu, CurrencyType.Zelkovyu);
    }
  }
}