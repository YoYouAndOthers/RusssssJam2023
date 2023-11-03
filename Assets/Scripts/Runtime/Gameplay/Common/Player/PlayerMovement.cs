using RussSurvivor.Runtime.Infrastructure.Inputs;
using UnityEngine;
using Zenject;

namespace RussSurvivor.Runtime.Gameplay.Common.Player
{
  public class PlayerMovement : MonoBehaviour
  {
    [SerializeField] private float _speed = 5f;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    private IInputService _inputService;

    [Inject]
    private void Construct(IInputService inputService)
    {
      _inputService = inputService;
    }

    private void Update()
    {
      Vector2 direction = GetIsometricDirection(_inputService.GetMovementInput());
      _rigidbody2D.velocity = direction * _speed;
    }

    private Vector2 GetIsometricDirection(Vector2 getMovementInput)
    {
      Vector2 direction = Vector2.zero;
      direction.x = getMovementInput.x;
      direction.y = getMovementInput.y * 2 / 3;
      return direction;
    }
  }
}