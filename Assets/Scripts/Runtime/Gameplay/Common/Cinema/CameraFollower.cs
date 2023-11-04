using RussSurvivor.Runtime.Gameplay.Common.Player;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Common.Cinema
{
  public class CameraFollower : MonoBehaviour
  {
    private PlayerBehaviourBase _player;

    private void Update()
    {
      transform.position = new Vector3(
        _player.transform.position.x,
        _player.transform.position.y,
        transform.position.z);
    }

    public void Initialize(PlayerBehaviourBase player)
    {
      _player = player;
      enabled = true;
    }

    public void Disable()
    {
      enabled = false;
    }
  }
}