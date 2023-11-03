using Cysharp.Threading.Tasks;
using RussSurvivor.Runtime.Gameplay.Battle.Weapons;
using RussSurvivor.Runtime.Gameplay.Common.Player;
using UnityEngine;
using Zenject;

namespace RussSurvivor.Runtime.Gameplay.Battle.Characters
{
  public class PlayerBattleSpawnPoint : MonoBehaviour
  {
    private IInstantiator _instantiator;
    private ICharacterRegistry _characterRegistry;
    private WeaponFactory _weaponFactory;

    [Inject]
    private void Construct(IInstantiator instantiator, ICharacterRegistry characterRegistry, WeaponFactory weaponFactory)
    {
      _instantiator = instantiator;
      _characterRegistry = characterRegistry;
      _weaponFactory = weaponFactory;
    }

    public async UniTask Initialize()
    {
      var config = await Resources.LoadAsync<PlayerConfig>("Configs/PlayerConfig") as PlayerConfig;
      if (config == null)
      {
        Debug.LogError("PlayerConfig not found!");
        return;
      }
      var player = _instantiator.InstantiatePrefabForComponent<PlayerBattleBehaviour>(
        config.BattlePrefab,
        transform.position,
        Quaternion.identity,
        null);

      _characterRegistry.RegisterPlayer(player);
      _weaponFactory.Create(Resources.Load<WeaponConfig>("Configs/Weapons/Weapon_Fists"), player);
    }
  }
}