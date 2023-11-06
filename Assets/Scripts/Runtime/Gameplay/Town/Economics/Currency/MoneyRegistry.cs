using System.Collections.Generic;
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

    public bool TrySpendMoney(CurrencyType currencyType, int amount, bool isBeaten = false)
    {
      if (!CanSpendMoney(currencyType, amount, isBeaten))
        return false;
      _money[currencyType] -= amount;
      return true;
    }

    public bool CanSpendMoney(CurrencyType currencyType, int amount, bool isBeaten = false)
    {
      Debug.Log($"Checking if can spend {amount} {currencyType} {(isBeaten ? "beaten" : "")}");
      if (isBeaten)
      {
        bool polushkiExist = _money.TryGetValue(CurrencyType.Polushka, out int polushka);
        bool zelkovyExists = _money.TryGetValue(CurrencyType.Zelkovyu, out int zelkovyu);
        bool chetvertushkaExists = _money.TryGetValue(CurrencyType.Chetvertushka, out int chetvertushka);
        bool serebryachokExists = _money.TryGetValue(CurrencyType.Serebryachok, out int serebryachok);

        var result = new Dictionary<CurrencyType, int>
        {
          [CurrencyType.Zelkovyu] = 0,

          [CurrencyType.Polushka] = polushkiExist
            ? polushka / 2 + polushka % 2 + (zelkovyExists ? zelkovyu : 0)
            : zelkovyExists
              ? zelkovyu
              : 0,

          [CurrencyType.Serebryachok] = 0,
          [CurrencyType.Chetvertushka] = serebryachokExists
            ? serebryachok / 2 + serebryachok % 2 + (chetvertushkaExists ? chetvertushka : 0)
            : chetvertushkaExists
              ? chetvertushka
              : 0
        };

        switch (currencyType)
        {
          case CurrencyType.Polushka:
          case CurrencyType.Chetvertushka:
            return result[currencyType] >= amount;
          case CurrencyType.Zelkovyu:
            return result[CurrencyType.Polushka] >= amount;
          default:
            return result[CurrencyType.Chetvertushka] >= amount;
        }
      }

      if (!_money.TryGetValue(currencyType, out int currentAmount))
        return false;
      return currentAmount >= amount;
    }
  }
}