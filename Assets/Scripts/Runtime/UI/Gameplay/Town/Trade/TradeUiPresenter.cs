using System.Linq;
using AYellowpaper.SerializedCollections;
using RussSurvivor.Runtime.Gameplay.Battle.Weapons;
using RussSurvivor.Runtime.Gameplay.Town.Economics.Currency;
using RussSurvivor.Runtime.Gameplay.Town.Economics.Trade;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RussSurvivor.Runtime.UI.Gameplay.Town.Trade
{
  public class TradeUiPresenter : MonoBehaviour, IInitializable
  {
    [SerializeField] private SerializedDictionary<CurrencyType, Sprite> _currencyIconByType = new();
    [SerializeField] private SerializedDictionary<CurrencyType, TextMeshProUGUI> _amountsByType = new();
    [SerializeField] private SerializedDictionary<CurrencyType, Image> _iconsByType = new();

    [SerializeField] private GameObject _weaponCostContainer;
    [SerializeField] private Transform[] _weaponSlots;

    [SerializeField] private RectTransform[] _weaponToByuSlots;
    [SerializeField] private Button _beatButton;
    private IInstantiator _instantiator;
    private IMoneyRegistry _moneyRegistry;
    private ITraderService _traderService;

    [Inject]
    private void Construct(IInstantiator instantiator, ITraderService traderService, IMoneyRegistry moneyRegistry)
    {
      _instantiator = instantiator;
      _traderService = traderService;
      _moneyRegistry = moneyRegistry;
    }

    public void Initialize()
    {
      _traderService.WeaponsForTrade
        .ObserveAdd()
        .Subscribe(OnWeaponAdded)
        .AddTo(this);

      _traderService.WeaponsForTrade
        .ObserveRemove()
        .Subscribe(OnWeaponRemoved)
        .AddTo(this);

      _traderService.IsTrading
        .ObserveEveryValueChanged(k => k.Value)
        .Subscribe(isTrading => gameObject.SetActive(isTrading))
        .AddTo(this);

      _beatButton.OnClickAsObservable()
        .Subscribe(_ => _traderService.IsBeaten.Value = true)
        .AddTo(this);

      _traderService.IsBeaten
        .ObserveEveryValueChanged(k => k.Value)
        .Subscribe(isBeaten => _beatButton.gameObject.SetActive(!isBeaten))
        .AddTo(this);

      _weaponCostContainer.SetActive(false);
      Debug.Log("Trade UI presenter initialized");
    }

    public void ShowWeaponCost(WeaponConfig weapon)
    {
      Debug.Log($"Show weapon cost {weapon.name}");
      foreach (Image image in _iconsByType.Values)
        image.gameObject.SetActive(false);
      foreach (TextMeshProUGUI text in _amountsByType.Values)
        text.gameObject.SetActive(false);

      _weaponCostContainer.SetActive(true);
      _iconsByType[weapon.CostType].gameObject.SetActive(true);
      _iconsByType[weapon.CostType].sprite = _currencyIconByType[weapon.CostType];
      _amountsByType[weapon.CostType].gameObject.SetActive(true);
      _amountsByType[weapon.CostType].text = weapon.CostAmount.ToString();
    }

    public void HideWeaponCost()
    {
      _weaponCostContainer.SetActive(false);
    }

    public bool TrySetWeaponBought(WeaponTradeUiItem item, WeaponConfig weapon)
    {
      if (_weaponToByuSlots.Any(slot =>
            RectTransformUtility.RectangleContainsScreenPoint(slot, item.transform.position) &&
            slot.GetComponent<InventoryContainerUi>().Size == weapon.Size))
      {
        item.transform.position = _weaponToByuSlots.First(slot =>
          RectTransformUtility.RectangleContainsScreenPoint(slot, item.transform.position)).position;
        _traderService.AddToCart(weapon);
        return true;
      }

      return false;
    }

    public bool TrySetWeaponBack(WeaponTradeUiItem weaponTradeUiItem, WeaponConfig weapon)
    {
      if (_weaponSlots.Select(k => k.GetComponent<RectTransform>()).Any(slot =>
            RectTransformUtility.RectangleContainsScreenPoint(slot, weaponTradeUiItem.transform.position)))
      {
        weaponTradeUiItem.transform.position = _weaponSlots.Select(k => k.GetComponent<RectTransform>()).First(slot =>
          RectTransformUtility.RectangleContainsScreenPoint(slot, weaponTradeUiItem.transform.position)).position;
        _traderService.RemoveFromCart(weapon);
        return true;
      }

      return false;
    }

    public void ShowReducedWeaponCost(WeaponConfig weapon)
    {
      Debug.Log($"Show weapon cost {weapon.name}");
      foreach (Image image in _iconsByType.Values)
        image.gameObject.SetActive(false);
      foreach (TextMeshProUGUI text in _amountsByType.Values)
        text.gameObject.SetActive(false);

      _weaponCostContainer.SetActive(true);
      if (weapon.CostType == CurrencyType.Zelkovyu)
        _iconsByType[CurrencyType.Polushka].gameObject.SetActive(true);
      else if (weapon.CostType == CurrencyType.Serebryachok)
        _iconsByType[CurrencyType.Chetvertushka].gameObject.SetActive(true);
      else
        _iconsByType[weapon.CostType].gameObject.SetActive(true);

      _amountsByType[weapon.CostType].gameObject.SetActive(true);
      _amountsByType[weapon.CostType].text = weapon.CostAmount.ToString();
    }

    private void OnWeaponAdded(CollectionAddEvent<WeaponConfig> weaponConfig)
    {
      WeaponConfig weapon = weaponConfig.Value;
      var item = _instantiator.InstantiatePrefabResourceForComponent<WeaponTradeUiItem>("Prefabs/UI/WeaponTradeUiItem",
        _weaponSlots[weaponConfig.Index]);
      item.Initialize(weapon, this, _traderService, _moneyRegistry);
      _traderService.IsBeaten
        .Where(l => l)
        .Subscribe(_ => item.UpdateAvailability(_moneyRegistry.CanSpendMoney(weapon.CostType, weapon.CostAmount, true)))
        .AddTo(this);

      _moneyRegistry.Money
        .ObserveReplace()
        .Where(k => k.Key == weapon.CostType)
        .Subscribe(_ =>
          item.UpdateAvailability(_moneyRegistry.CanSpendMoney(weapon.CostType, weapon.CostAmount,
            _traderService.IsBeaten.Value)))
        .AddTo(this);
    }

    private void OnWeaponRemoved(CollectionRemoveEvent<WeaponConfig> weaponConfig)
    {
      foreach (Transform child in _weaponSlots[weaponConfig.Index])
        Destroy(child.gameObject);
    }
  }
}