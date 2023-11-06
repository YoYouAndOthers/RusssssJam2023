using RussSurvivor.Runtime.Gameplay.Battle.Weapons;
using RussSurvivor.Runtime.Gameplay.Town.Economics.Currency;
using RussSurvivor.Runtime.Gameplay.Town.Economics.Trade;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.UI;

namespace RussSurvivor.Runtime.UI.Gameplay.Town.Trade
{
  [RequireComponent(typeof(OnScreenControl))]
  public class WeaponTradeUiItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler,
    IBeginDragHandler, IEndDragHandler
  {
    [SerializeField] private Image _weaponImage;
    private Vector2 _delta;
    private bool _dragged;
    private IMoneyRegistry _moneyRegistry;
    private Vector3 _startPosition;
    private ITraderService _traderService;
    private TradeUiPresenter _tradeUiPresenter;
    private WeaponConfig _weapon;

    public void OnBeginDrag(PointerEventData eventData)
    {
      _startPosition = transform.position;
      if (!_moneyRegistry.CanSpendMoney(_weapon.CostType, _weapon.CostAmount, _traderService.IsBeaten.Value))
        return;

      _delta = (Vector2)transform.position - eventData.position;
      _dragged = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
      if (_dragged)
        transform.position = eventData.position + _delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
      _dragged = false;
      if (_traderService.IsInCart(_weapon))
        if (!_tradeUiPresenter.TrySetWeaponBack(this, _weapon))
          transform.position = _startPosition;

      if (!_tradeUiPresenter.TrySetWeaponBought(this, _weapon))
        transform.position = _startPosition;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
      if (!_traderService.IsBeaten.Value)
        _tradeUiPresenter.ShowWeaponCost(_weapon);
      else
        _tradeUiPresenter.ShowReducedWeaponCost(_weapon);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
      _tradeUiPresenter.HideWeaponCost();
    }

    public void Initialize(WeaponConfig weapon, TradeUiPresenter tradeUiPresenter, ITraderService traderService,
      IMoneyRegistry moneyRegistry)
    {
      _traderService = traderService;
      _moneyRegistry = moneyRegistry;
      _weapon = weapon;
      _tradeUiPresenter = tradeUiPresenter;
      Debug.Log($"Weapon trade UI item initialized with weapon {weapon.name}");
      _weaponImage.sprite = weapon.Icon;
      _weaponImage.SetNativeSize();
      UpdateAvailability(_moneyRegistry.CanSpendMoney(_weapon.CostType, weapon.CostAmount));
    }

    public void UpdateAvailability(bool canSpendMoney)
    {
      transform.parent.GetComponent<Image>().color =
        canSpendMoney ? new Color(0, 0.9f, 0, 0.2f) : new Color(0.9f, 0, 0, 0.2f);
    }
  }
}