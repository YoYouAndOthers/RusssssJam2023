using System;
using UnityEngine;

namespace RussSurvivor.Runtime.Infrastructure.Inputs
{
  public class InputService : IInputService
  {
    private readonly InputControls _inputControls = new();
    public Action OnConsoleCalled { get; set; }

    public void Initialize()
    {
      _inputControls.Enable();
      _inputControls.Debug.CallConsole.performed += _ => OnConsoleCalled?.Invoke();
      Debug.Log("Input service initialized");
    }

    public Vector2 GetMovementInput()
    {
      return _inputControls.Gameplay.Movement.ReadValue<Vector2>();
    }
  }
}