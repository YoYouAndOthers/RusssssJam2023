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
    private IInstantiator _instantiator;
    private ITraderService _traderService;

    [SerializeField] private TextMeshProUGUI _weaponCostAmountText;
    [SerializeField] private TextMeshProUGUI _weaponCostTypeText;

    [SerializeField] private SerializedDictionary<CurrencyType, GameObject> _currencyTypeToIcon = new();
    [SerializeField] private SerializedDictionary<CurrencyType, string> _currencyDescriptionTexts = new();

    [SerializeField] private GameObject _weaponCostContainer;
    [SerializeField] private Transform[] _weaponSlots;

    [SerializeField] private RectTransform[] _weaponToByuSlots;
    private IMoneyRegistry _moneyRegistry;

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

      _weaponCostContainer.SetActive(false);
      Debug.Log("Trade UI presenter initialized");
    }

    private void OnWeaponAdded(CollectionAddEvent<WeaponConfig> weaponConfig)
    {
      WeaponConfig weapon = weaponConfig.Value;
      var item = _instantiator.InstantiatePrefabResourceForComponent<WeaponTradeUiItem>("Prefabs/UI/WeaponTradeUiItem",
        _weaponSlots[weaponConfig.Index]);
      item.Initialize(weapon, this, _moneyRegistry);
      _moneyRegistry.Money
        .ObserveReplace()
        .Where(k => k.Key == weapon.CostType)
        .Subscribe(_ => item.UpdateAvailability(_moneyRegistry.CanSpendMoney(weapon.CostType, weapon.CostAmount)))
        .AddTo(item);
    }

    private void OnWeaponRemoved(CollectionRemoveEvent<WeaponConfig> weaponConfig)
    {
      foreach (Transform child in _weaponSlots[weaponConfig.Index])
        Destroy(child.gameObject);
    }

    public void ShowWeaponCost(WeaponConfig weapon)
    {
      foreach (GameObject obj in _currencyTypeToIcon.Values)
        obj.SetActive(false);
      _currencyTypeToIcon[weapon.CostType].SetActive(true);
      _weaponCostAmountText.text = weapon.CostAmount.ToString();
      _weaponCostTypeText.text = _currencyDescriptionTexts[weapon.CostType];
      _weaponCostContainer.SetActive(true);

      LayoutRebuilder.ForceRebuildLayoutImmediate(_weaponCostContainer.GetComponent<RectTransform>());
      Canvas.ForceUpdateCanvases();
    }

    public void HideWeaponCost()
    {
      _weaponCostContainer.SetActive(false);
    }

    public bool TrySetWeaponBought(Vector3 transformPosition, WeaponConfig weapon)
    {
      if (_weaponToByuSlots.Any(slot => RectTransformUtility.RectangleContainsScreenPoint(slot, transformPosition)))
      {
        _traderService.AddToCart(weapon);
        return true;
      }

      return false;
    }
  }
}