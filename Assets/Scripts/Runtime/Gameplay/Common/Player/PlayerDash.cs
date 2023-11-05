using RussSurvivor.Runtime.Infrastructure.Inputs;
using UnityEngine;
using Zenject;

namespace RussSurvivor.Runtime.Gameplay.Common.Player
{
  public class PlayerDash : MonoBehaviour
  {
    [SerializeField] private float _dashSpeed = 10f;
    [SerializeField] private float _dashDuration = 0.2f;
    [SerializeField] private float _dashCooldown = 1f;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    private float _dashCooldownTimer;
    private float _dashTimer;
    private IInputService _inputService;

    [Inject]
    private void Construct(IInputService inputService)
    {
      _inputService = inputService;
    }

    private void Awake()
    {
      _inputService.OnDashCalled += OnDashCalled;
    }

    private void Update()
    {
      if (_dashCooldownTimer > 0)
        _dashCooldownTimer -= Time.deltaTime;

      if (_dashTimer > 0)
      {
        _dashTimer -= Time.deltaTime;
        _rigidbody2D.velocity = _rigidbody2D.velocity.normalized * _dashSpeed;
      }
    }

    private void OnDashCalled()
    {
      if (_dashCooldownTimer > 0)
        return;

      _dashTimer = _dashDuration;
      _dashCooldownTimer = _dashCooldown;
    }
  }
}