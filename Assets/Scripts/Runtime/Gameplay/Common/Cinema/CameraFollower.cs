using RussSurvivor.Runtime.Gameplay.Battle.Characters;
using RussSurvivor.Runtime.Gameplay.Common.Player;
using UnityEngine;
using Zenject;

namespace RussSurvivor.Runtime.Gameplay.Common.Cinema
{
  public class CameraFollower : MonoBehaviour, IInitializable
  {
    private ICharacterRegistry _characterRegistry;
    private PlayerBehaviour _player;

    [Inject]
    private void Construct(ICharacterRegistry characterRegistry)
    {
      _characterRegistry = characterRegistry;
    }

    public void Initialize()
    {
      _player = _characterRegistry.GetPlayer();
      enabled = true;
    }

    private void Update()
    {
      transform.position = new Vector3(
        _player.transform.position.x,
        _player.transform.position.y,
        transform.position.z);
    }
  }
}