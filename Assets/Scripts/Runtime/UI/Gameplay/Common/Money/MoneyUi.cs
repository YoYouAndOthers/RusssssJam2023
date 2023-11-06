using AYellowpaper.SerializedCollections;
using RussSurvivor.Runtime.Gameplay.Town.Economics.Currency;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace RussSurvivor.Runtime.UI.Gameplay.Common.Money
{
  public class MoneyUi : MonoBehaviour
  {
    [SerializeField] private SerializedDictionary<CurrencyType, TextMeshProUGUI> _currencyIconByType = new();
    private IMoneyRegistry _moneyRegistry;

    [Inject]
    private void Construct(IMoneyRegistry moneyRegistry)
    {
      _moneyRegistry = moneyRegistry;
    }
    
    private void Awake()
    {
      _moneyRegistry.Money.ObserveAdd()
        .Subscribe(OnMoneyChanged)
        .AddTo(this);
      
      _moneyRegistry.Money.ObserveReplace()
        .Subscribe(OnMoneyChangedReplacaed)
        .AddTo(this);
    }

    private void OnMoneyChanged( DictionaryAddEvent<CurrencyType, int> money)
    {
      foreach (CurrencyType currency in _currencyIconByType.Keys)
      {
        if(_moneyRegistry.Money.TryGetValue(currency, out int amount))
          _currencyIconByType[currency].text = amount.ToString();
        else
          _currencyIconByType[currency].text = "0";
      }
    }
    
    private void OnMoneyChangedReplacaed( DictionaryReplaceEvent<CurrencyType, int> money)
    {
      foreach (CurrencyType currency in _currencyIconByType.Keys)
      {
        if(_moneyRegistry.Money.TryGetValue(currency, out int amount))
          _currencyIconByType[currency].text = amount.ToString();
        else
          _currencyIconByType[currency].text = "0";
      }
    }
  }
}