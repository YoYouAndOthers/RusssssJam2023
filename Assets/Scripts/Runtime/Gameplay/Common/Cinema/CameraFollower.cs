using RussSurvivor.Runtime.Gameplay.Common.Player;
using UnityEngine;
using Zenject;

namespace RussSurvivor.Runtime.Gameplay.Common.Cinema
{
  public class CameraFollower : MonoBehaviour, IInitializable
  {
    private IPlayerRegistry _characterRegistry;
    private PlayerBehaviourBase _player;

    [Inject]
    private void Construct(IPlayerRegistry characterRegistry)
    {
      _characterRegistry = characterRegistry;
    }

    private void Update()
    {
      transform.position = new Vector3(
        _player.transform.position.x,
        _player.transform.position.y,
        transform.position.z);
    }

    public void Initialize()
    {
      _player = _characterRegistry.GetPlayer();
      enabled = true;
    }
  }
}