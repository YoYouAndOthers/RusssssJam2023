using System.Collections;
using UnityEngine;
using FMODUnity;
using RussSurvivor.Runtime.Infrastructure.Inputs;
using Zenject;

namespace RussSurvivor.Runtime.Gameplay.Common.Player
{
  public class PlayerDash : MonoBehaviour
  {
    [SerializeField] private float _dashSpeed = 10f;
    [SerializeField] private float _dashDelay = 0.2f;
    [SerializeField] private float _dashDuration = 0.2f;
    [SerializeField] private float _dashCooldown = 1f;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    private float _dashCooldownTimer;
    private float _dashTimer;
    private IInputService _inputService;
    [SerializeField] private CharecterViewController _view;
    private Vector2 dashVector;

    // Add the FMOD event path
    [EventRef] [SerializeField]
    private string dashEventPath = "event:/Dash 1"; // Set this to the path of your FMOD event

    private FMOD.Studio.EventInstance dashEventInstance;

    [Inject]
    private void Construct(IInputService inputService)
    {
      _inputService = inputService;
    }

    private void Awake()
    {
      _inputService.OnDashCalled += OnDashCalled;
      _view.dashDuration = _dashDuration;
      _view.dashDelay = _dashDelay;
    }

    private void Update()
    {
      if (_dashCooldownTimer > 0)
        _dashCooldownTimer -= Time.deltaTime;

      if (_dashTimer > 0)
      {
        _dashTimer -= Time.deltaTime;
        _rigidbody2D.velocity = dashVector * _dashSpeed;
      }
    }

    private void OnDashCalled()
    {
      dashVector = _rigidbody2D.velocity.normalized;
      if (_dashCooldownTimer > 0 || dashVector.magnitude < 0.01f)
        return;

      _view?.PlayAnimation(CharecterViewController.AnimationState.Dash, dashVector);

      // Trigger the FMOD event
      RuntimeManager.PlayOneShot(dashEventPath, transform.position);

      StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
      yield return new WaitForSeconds(_dashDelay);
      _dashTimer = _dashDuration;
      _dashCooldownTimer = _dashCooldown;
    }
  }
}