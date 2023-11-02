using System;
using UnityEngine;
using Zenject;

namespace RussSurvivor.Runtime.Infrastructure.Inputs
{
  public interface IInputService : IInitializable
  {
    Action OnConsoleCalled { get; set; }
    Vector2 GetMovementInput();
  }
}