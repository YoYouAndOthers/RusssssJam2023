using Cysharp.Threading.Tasks;
using RussSurvivor.Runtime.Gameplay.Common.Player;
using UnityEngine;
using Zenject;

namespace RussSurvivor.Runtime.Gameplay.Battle.Characters
{
  public class PlayerBattleSpawnPoint : MonoBehaviour
  {
    private IInstantiator _instantiator;
    private ICharacterRegistry _characterRegistry;

    [Inject]
    private void Construct(IInstantiator instantiator, ICharacterRegistry characterRegistry)
    {
      _instantiator = instantiator;
      _characterRegistry = characterRegistry;
    }

    public async void Initialize()
    {
      
    }
    
    public async void Awake()
    {
      var config = await Resources.LoadAsync<PlayerConfig>("Configs/PlayerConfig") as PlayerConfig;
      if (config == null)
      {
        Debug.LogError("PlayerConfig not found!");
        return;
      }
      var player = _instantiator.InstantiatePrefabForComponent<PlayerBehaviour>(
        config.BattlePrefab,
        transform.position,
        Quaternion.identity,
        null);

      _characterRegistry.RegisterPlayer(player);
    }
  }
}