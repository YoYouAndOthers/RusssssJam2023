using RussSurvivor.Runtime.Gameplay.Common.Player;
using UnityEngine;
using Zenject;

namespace RussSurvivor.Runtime.Gameplay.Battle.Characters
{
  public class PlayerSpawnPoint : MonoBehaviour
  {
    [SerializeField] private PlayerPrefabType _playerPrefabType;
    
    private IInstantiator _instantiator;
    private IPlayerPrefabProvider _prefabProvider;
    private IPlayerRegistry _characterRegistry;

    [Inject]
    private void Construct(
      IInstantiator instantiator,
      IPlayerPrefabProvider prefabProvider,
      IPlayerRegistry playerRegistry)
    {
      _instantiator = instantiator;
      _prefabProvider = prefabProvider;
      _characterRegistry = playerRegistry;
    }

    public void Initialize()
    {
      var player = _instantiator.InstantiatePrefabForComponent<PlayerBehaviourBase>(
        _prefabProvider.GetPlayerPrefab(_playerPrefabType),
        transform.position,
        Quaternion.identity,
        null);

      _characterRegistry.RegisterPlayer(player);
    }
  }
}