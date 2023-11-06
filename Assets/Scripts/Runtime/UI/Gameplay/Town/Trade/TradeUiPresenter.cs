using RussSurvivor.Runtime.Gameplay.Battle.Weapons;
using RussSurvivor.Runtime.Gameplay.Town.Economics.Trade;
using UniRx;
using UnityEngine;
using Zenject;

namespace RussSurvivor.Runtime.UI.Gameplay.Town.Trade
{
  public class TradeUiPresenter : MonoBehaviour, IInitializable
  {
    private IInstantiator _instantiator;
    private ITraderService _traderService;

    [SerializeField] private Transform[] _weaponSlots;

    [Inject]
    private void Construct(IInstantiator instantiator, ITraderService traderService)
    {
      _instantiator = instantiator;
      _traderService = traderService;
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
      Debug.Log("Trade UI presenter initialized");
    }

    private void OnWeaponAdded(CollectionAddEvent<WeaponConfig> weaponConfig)
    {
      WeaponConfig weapon = weaponConfig.Value;
      var item = _instantiator.InstantiatePrefabResourceForComponent<WeaponTradeUiItem>("Prefabs/UI/WeaponTradeUiItem",
        _weaponSlots[weaponConfig.Index]);
      item.Initialize(weapon);
    }

    private void OnWeaponRemoved(CollectionRemoveEvent<WeaponConfig> weaponConfig)
    {
      foreach (Transform child in _weaponSlots[weaponConfig.Index])
        Destroy(child.gameObject);
    }
  }
}