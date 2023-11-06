using System.Collections;
using System.Collections;
using UnityEngine;
using Zenject;
using FMODUnity; // Import the FMODUnity namespace
using RussSurvivor.Runtime.Infrastructure.Inputs; // Import the InputService namespace

namespace RussSurvivor.Runtime.Gameplay.Common.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private CharecterViewController _view;
        private Vector2 lastViewDirection;

        private IInputService _inputService;

        // Add the FMOD event path for footsteps
        [EventRef]
        [SerializeField] private string footstepsEventPath = "event:/Footsteps"; // Set this to the path of your FMOD footsteps event

        private float footstepsCooldown = 0.3f; // Adjust the cooldown duration as needed
        private float timeSinceLastFootstep = 0f;

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

                    // Check if enough time has passed since the last footstep
                    if (Time.time - timeSinceLastFootstep > footstepsCooldown)
                    {
                        // Trigger FMOD footsteps event
                        RuntimeManager.PlayOneShot(footstepsEventPath, transform.position);
                        timeSinceLastFootstep = Time.time; // Update the time of the last footstep
                    }
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