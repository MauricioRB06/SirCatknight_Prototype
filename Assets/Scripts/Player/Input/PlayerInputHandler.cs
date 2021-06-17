﻿using UnityEngine;
using UnityEngine.InputSystem;

/* Documentation:
 * 
 * Input System: https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/QuickStartGuide.html
 * Math ABS: https://docs.microsoft.com/en-us/dotnet/api/system.math.abs?view=net-5.0
 * Screen To World Point: https://docs.unity3d.com/ScriptReference/Camera.ScreenToWorldPoint.html
 * 
 */

namespace Player.Input
{
    public class PlayerInputHandler : MonoBehaviour
    {
        private PlayerInput _playerInput;
        private Camera _playerCamera;
        
        //
        private Vector2 RawMovementInput { get; set; }
        private Vector2 RawDashDirectionInput { get; set; }
        public Vector2Int DashDirectionInput { get; private set; }

        // We use them to perform the normalized movement
        public int NormInputX { get; private set; }
        public int NormInputY { get; private set; }
        
        // We use them to configure the total duration of the jump based on the duration of the button press
        public bool JumpInput { get; private set; }
        public bool JumpInputStop { get; private set; }
        
        // We use it to check if the button is being pressed to grab the wall
        public bool GrabInput { get; private set; }
        
        // We use them to configure the total duration of the Dash based on the duration of the button press
        public bool DashInput { get; private set; }
        public bool DashInputStop { get; private set; }
        
        // Waiting time to be able to press a button again
        [SerializeField]
        private float inputHoldTime = 0.2f;
        
        // 
        private float _jumpInputStartTime;
        private float _dashInputStartTime;

        private void Start()
        {
            _playerInput = GetComponent<PlayerInput>();
            _playerCamera = Camera.main;
        }

        private void Update()
        {
            CheckJumpInputHoldTime();
            CheckDashInputHoldTime();
        }

        public void OnMoveInput(InputAction.CallbackContext context)
        {
            RawMovementInput = context.ReadValue<Vector2>();

            if(Mathf.Abs(RawMovementInput.x) > 0.5f)
            {
                // We normalize the movement to standardize the controls
                NormInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
            }
            else
            {
                NormInputX = 0;
            }
        
            if(Mathf.Abs(RawMovementInput.y) > 0.5f)
            {
                // We normalize the movement to standardize the controls
                NormInputY = (int)(RawMovementInput * Vector2.up).normalized.y;
            }
            else
            {
                NormInputY = 0;
            }
        }

        public void OnJumpInput(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                JumpInput = true;
                JumpInputStop = false;
                _jumpInputStartTime = Time.time;
            }

            if (context.canceled)
            {
                JumpInputStop = true;
            }
        }

        public void OnGrabInput(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                GrabInput = true;
            }

            if (context.canceled)
            {
                GrabInput = false;
            }
        }

        public void OnDashInput(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                DashInput = true;
                DashInputStop = false;
                _dashInputStartTime = Time.time;
            }
            else if (context.canceled)
            {
                DashInputStop = true;
            }
        }

        public void OnDashDirectionInput(InputAction.CallbackContext context)
        {
            RawDashDirectionInput = context.ReadValue<Vector2>();

            if(_playerInput.currentControlScheme == "Keyboard")
            {
                RawDashDirectionInput = _playerCamera.ScreenToWorldPoint(RawDashDirectionInput) - transform.position;
            }

            DashDirectionInput = Vector2Int.RoundToInt(RawDashDirectionInput.normalized);
        }
        
        // We use it to set the jump to false, after we use it
        public void UseJumpInput() => JumpInput = false;
        
        // 
        public void UseDashInput() => DashInput = false;
        
        // We use it to prevent unwanted jumps from being made
        private void CheckJumpInputHoldTime()
        {
            if(Time.time >= _jumpInputStartTime + inputHoldTime)
            {
                JumpInput = false;
            }
        }

        private void CheckDashInputHoldTime()
        {
            if(Time.time >= _dashInputStartTime + inputHoldTime)
            {
                DashInput = false;
            }
        }
    }
}
