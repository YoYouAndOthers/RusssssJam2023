using RussSurvivor.Runtime.Gameplay.Battle.Weapons;
using RussSurvivor.Runtime.Gameplay.Town.Economics.Currency;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.UI;

namespace RussSurvivor.Runtime.UI.Gameplay.Town.Trade
{
  [RequireComponent(typeof(OnScreenControl))]
  public class WeaponTradeUiItem : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
  {
    [SerializeField] private Image _weaponImage;
    private TradeUiPresenter _tradeUiPresenter;
    private WeaponConfig _weapon;
    private IMoneyRegistry _moneyRegistry;

    public void Initialize(WeaponConfig weapon, TradeUiPresenter tradeUiPresenter, IMoneyRegistry moneyRegistry)
    {
      _moneyRegistry = moneyRegistry;
      _weapon = weapon;
      _tradeUiPresenter = tradeUiPresenter;
      Debug.Log($"Weapon trade UI item initialized with weapon {weapon.name}");
      _weaponImage.sprite = weapon.Icon;
      _weaponImage.SetNativeSize();
      UpdateAvailability(_moneyRegistry.CanSpendMoney(_weapon.CostType, weapon.CostAmount));
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
      _tradeUiPresenter.ShowWeaponCost(_weapon);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
      _tradeUiPresenter.HideWeaponCost();
    }

    public void UpdateAvailability(bool canSpendMoney)
    {
      _weaponImage.color = canSpendMoney ?
        new Color(0, 0.9f, 0, 0.2f) : 
        new Color(0.9f, 0, 0, 0.2f);
    }
  }
}