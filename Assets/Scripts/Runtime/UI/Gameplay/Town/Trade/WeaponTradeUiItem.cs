using RussSurvivor.Runtime.Gameplay.Battle.Weapons;
using UnityEngine;
using UnityEngine.UI;

namespace RussSurvivor.Runtime.UI.Gameplay.Town.Trade
{
  public class WeaponTradeUiItem : MonoBehaviour
  {
    [SerializeField] private Image _weaponImage;
    
    public void Initialize(WeaponConfig weapon)
    {
      Debug.Log($"Weapon trade UI item initialized with weapon {weapon.name}");
      _weaponImage.sprite = weapon.Icon;
      _weaponImage.SetNativeSize();
    }
  }
}