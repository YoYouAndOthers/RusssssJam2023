using RussSurvivor.Runtime.Application.Progress.Data;
using RussSurvivor.Runtime.Application.Progress.Watchers;
using UnityEngine;
using Zenject;

namespace RussSurvivor.Runtime.Gameplay.Common.Player
{
  public class PlayerBehaviour : MonoBehaviour, IProgressWriter
  {
    [SerializeField] private PlayerMovement _playerMovement;

    [Inject]
    private void Construct(IProgressWatcherService progressWatcherService)
    {
      progressWatcherService.Register(this);
    }

    public void Load(GameProgress progress)
    {
      transform.position = new Vector3(progress.PlayerPosition.x, progress.PlayerPosition.y, transform.position.z);
    }

    public void Save(GameProgress progress)
    {
      progress.PlayerPosition = new Vector2Data
      {
        x = transform.position.x,
        y = transform.position.y
      };
    }
  }
}