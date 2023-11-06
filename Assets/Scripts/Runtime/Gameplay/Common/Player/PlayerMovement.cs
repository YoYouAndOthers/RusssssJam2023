using RussSurvivor.Runtime.Infrastructure.Inputs;
using UnityEngine;
using Zenject;

namespace RussSurvivor.Runtime.Gameplay.Common.Player
{
  public class PlayerMovement : MonoBehaviour
  {
    [SerializeField] private float _speed = 5f;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    
    [SerializeField] private CharecterViewController _view;
    private Vector2 lastViewDirection;
    
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

      if (_view)
      {
        if (direction.magnitude > 0.3f)
        {
          lastViewDirection = direction;
          _view.PlayAnimation(CharecterViewController.AnimationState.Run, lastViewDirection);
        }
        else
        {
          _view.PlayAnimation(CharecterViewController.AnimationState.Idle, lastViewDirection);
        }
      }
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