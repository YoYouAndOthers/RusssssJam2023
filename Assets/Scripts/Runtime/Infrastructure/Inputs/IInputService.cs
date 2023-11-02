using UnityEngine;
using Zenject;

namespace RussSurvivor.Runtime.Infrastructure.Inputs
{
  public interface IInputService : IInitializable
  {
    Vector2 GetMovementInput();
  }
}