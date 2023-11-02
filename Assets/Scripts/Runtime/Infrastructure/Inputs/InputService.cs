using UnityEngine;

namespace RussSurvivor.Runtime.Infrastructure.Inputs
{
  public class InputService : IInputService
  {
    private readonly InputControls _inputControls = new();

    public void Initialize()
    {
      _inputControls.Enable();
      Debug.Log("Input service initialized");
    }

    public Vector2 GetMovementInput()
    {
      return _inputControls.Gameplay.Movement.ReadValue<Vector2>();
    }
  }
}