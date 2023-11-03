using RussSurvivor.Runtime.Gameplay.Battle.Characters;
using RussSurvivor.Runtime.Gameplay.Common.Player;
using UnityEngine;
using Zenject;

namespace RussSurvivor.Runtime.Gameplay.Common.Cinema
{
  public class CameraFollower : MonoBehaviour, IInitializable
  {
    private ICharacterRegistry _characterRegistry;
    private PlayerBattleBehaviour _playerBattle;

    [Inject]
    private void Construct(ICharacterRegistry characterRegistry)
    {
      _characterRegistry = characterRegistry;
    }

    private void Update()
    {
      transform.position = new Vector3(
        _playerBattle.transform.position.x,
        _playerBattle.transform.position.y,
        transform.position.z);
    }

    public void Initialize()
    {
      _playerBattle = _characterRegistry.GetPlayer();
      enabled = true;
    }
  }
}