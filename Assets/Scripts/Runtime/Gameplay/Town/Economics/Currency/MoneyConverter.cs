using System.Collections.Generic;

namespace RussSurvivor.Runtime.Gameplay.Town.Economics.Currency
{
  public static class MoneyConverter
  {
    public const int MoneyValueFromChetvertushka = 1;
    public const int MoneyValueFromSerebryachok = 2;
    public const int MoneyValueFromPolushka = 128;
    public const int MoneyValueFromZelkovyu = 256;

    private static readonly Dictionary<CurrencyType, int> MoneyValues = new()
    {
      [CurrencyType.Chetvertushka] = MoneyValueFromChetvertushka,
      [CurrencyType.Serebryachok] = MoneyValueFromSerebryachok,
      [CurrencyType.Polushka] = MoneyValueFromPolushka,
      [CurrencyType.Zelkovyu] = MoneyValueFromZelkovyu
    };

    public static int ConvertToChetvertushka(int amount, CurrencyType currencyType)
    {
      return amount * MoneyValues[currencyType];
    }
  }
}