using RussSurvivor.Runtime.Gameplay.Battle.Weapons;
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

    public void Initialize(WeaponConfig weapon, TradeUiPresenter tradeUiPresenter)
    {
      _weapon = weapon;
      _tradeUiPresenter = tradeUiPresenter;
      Debug.Log($"Weapon trade UI item initialized with weapon {weapon.name}");
      _weaponImage.sprite = weapon.Icon;
      _weaponImage.SetNativeSize();
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
  }
}