using System;
using UnityEngine;
using UnityEngine.InputSystem;

// The purpose of this Script is:
/* Insert Here */

/* Documentation and References:
 * 
 * Input System: https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/QuickStartGuide.html
 * Math ABS: https://docs.microsoft.com/en-us/dotnet/api/system.math.abs?view=net-5.0
 * Screen To World Point: https://docs.unity3d.com/ScriptReference/Camera.ScreenToWorldPoint.html
 * Enumeration Class: https://docs.microsoft.com/en-us/dotnet/api/system.enum?view=net-5.0
 * Enumeration Types: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/enum
 * Curse C# Enum: https://www.youtube.com/watch?v=gj1AA7ZkuSc [ Spanish ]
 * 
 */

public enum CombatInputs
{
    PrimaryAttackInput,
    SecondaryAttackInput
}

namespace Player.Input
{
    public class PlayerInputHandler : MonoBehaviour
    {
        //We will use it to store the initial value of the movement raw input
        private Vector2 RawMovementInput { get; set; }
        
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
        
        // We use them to check if the Dodge button is pressed
        public bool DodgeRollInput { get; private set; }
        private float _dodgeRollInputTime;

        // We use them to get a reference to the control scheme we are using 
        private PlayerInput _playerInput;
        
        // We use it to take the camera as a reference and position the DashInput within the player's screen
        private Camera _playerCamera;
        
        // We will use it to store the initial value of the raw input
        private Vector2 RawDashDirectionInput { get; set; }
        
        // We use it to store the normalized address of the RawDashInput 
        public Vector2Int DashDirectionInput { get; private set; }
        
        // Waiting time to be able to press a button again
        [SerializeField] private float inputHoldTime = 0.2f;
        
        // We use it to control the behavior of Dash and Jump skills based on the time the player presses the button
        private float _jumpInputStartTime;
        private float _dashInputStartTime;
        
        // We use it to store the amount of attack inputs the player has
        public bool[] AttackInputs { get; private set; }
        
        // 
        public bool Interact { get; private set; }
        
        // 
        private bool ControllerCanMove { get; set; }
        private bool ControllerCanJump { get; set; }
        private bool ControllerCanAttack { get; set; }
        private bool ControllerCanDash { get; set; }
        private bool ControllerCanDodgeRoll { get; set; }
        private bool ControllerCanGrab { get; set; }
        public bool ControllerCanCrouch { get; private set; }
        public bool ControllerCanWallSlide { get; private set; }
        public bool ControllerCanLedgeClimb { get; private set; }
        
        // 
        private void Awake()
        {
            ControllerCanMove = true;
            ControllerCanCrouch = true;
            ControllerCanJump = true;
            ControllerCanAttack = true;
            ControllerCanDash = true;
            ControllerCanDodgeRoll = true;
            ControllerCanGrab = true;
            ControllerCanWallSlide = true;
            ControllerCanLedgeClimb = true;
        }
        
        // 
        private void Start()
        {
            var attackInputCounter = Enum.GetValues(typeof(CombatInputs)).Length;
            AttackInputs = new bool[attackInputCounter];
            
            _playerInput = GetComponent<PlayerInput>();
            _playerCamera = Camera.main;
        }
        
        // 
        private void Update()
        {
            CheckJumpInputHoldTime();
            CheckDashInputHoldTime();
        }
        
        // 
        public void OnMoveInput(InputAction.CallbackContext context)
        {
            if (!ControllerCanMove) return;
            
            RawMovementInput = context.ReadValue<Vector2>();
            
            /* We use a minimum tolerance of movement of the controls, to make sure that the player really wants to
               move and to avoid errors in the animator when it is wall climb due to small changes in the input values
               of the control */

            NormInputX = Mathf.RoundToInt(RawMovementInput.x);
            NormInputY = Mathf.RoundToInt(RawMovementInput.y);
        }
        
        // 
        public void OnJumpInput(InputAction.CallbackContext context)
        {
            if (!ControllerCanJump) return;
            
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
        
        // 
        public void OnGrabInput(InputAction.CallbackContext context)
        {
            if (!ControllerCanGrab) return;
            
            if (context.started) GrabInput = true;

            if (context.canceled) GrabInput = false;
        }
        
        // 
        public void OnDashInput(InputAction.CallbackContext context)
        {
            if (!ControllerCanDash) return;
            
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
        
        // 
        public void OnDashDirectionInput(InputAction.CallbackContext context)
        {
            if (!ControllerCanDash) return;
            
            RawDashDirectionInput = context.ReadValue<Vector2>();

            if(_playerInput.currentControlScheme == "Keyboard")
            {
                // We convert our raw input, into a vector that points from our player, towards the mouse position
                RawDashDirectionInput = _playerCamera.ScreenToWorldPoint(RawDashDirectionInput) - transform.position;
            }
            
            // We normalize the RawDashInput to obtain values that are traversed in 45° positions
            DashDirectionInput = Vector2Int.RoundToInt(RawDashDirectionInput.normalized);
        }
        
        // 
        public void OnDodgeRollInput(InputAction.CallbackContext context)
        {
            if (!ControllerCanDodgeRoll) return;

            if (context.started)
            {
                DodgeRollInput = true;
                _dodgeRollInputTime = Time.time;
            }
        }
        
        // 
        public void OnPrimaryAttackInput(InputAction.CallbackContext context)
        {
            if (!ControllerCanAttack) return;
            
            if (context.started)
            {
                AttackInputs[(int)CombatInputs.PrimaryAttackInput] = true;
            }
            else if (context.canceled)
            {
                AttackInputs[(int)CombatInputs.PrimaryAttackInput] = false;
            }
        }
        
        // 
        public void OnSecondaryAttackInput(InputAction.CallbackContext context)
        {
            if (!ControllerCanAttack) return;
            
            if (context.started)
            {
                AttackInputs[(int)CombatInputs.SecondaryAttackInput] = true;
            }
            else if (context.canceled)
            {
                AttackInputs[(int)CombatInputs.SecondaryAttackInput] = false;
            }
        }
        
        // 
        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                Interact = true;
            }
            else if (context.canceled)
            {
                Interact = false;
            }
        }
        
        // We use it to set the JumpInput to false, after using it
        public void UseJumpInput() => JumpInput = false;
        
        // We use it to set the DashInput to false, after using it
        public void UseDashInput() => DashInput = false;
        
        //We use it to set the DodgeRollInput to false, after using it
        public void UseDodgeRollInput() => DodgeRollInput = false;
        
        //
        public float DodgeRollInputTime() => _dodgeRollInputTime;
        
        // We use it to prevent unwanted jumps from being made
        private void CheckJumpInputHoldTime()
        {
            if (Time.time >= _jumpInputStartTime + inputHoldTime) { JumpInput = false; }
        }
        
        private void CheckDashInputHoldTime()
        {
            if(Time.time >= _dashInputStartTime + inputHoldTime) { DashInput = false; }
        }
        
        // 
        public void ChangeControllerCanMove() => ControllerCanMove = !ControllerCanMove;
        public void ChangeControllerCanCrouch() => ControllerCanCrouch = !ControllerCanCrouch;
        public void ChangeControllerCanJump() => ControllerCanJump = !ControllerCanJump;
        public void ChangeControllerCanAttack() => ControllerCanAttack = !ControllerCanAttack;
        public void ChangeControllerCanDash() => ControllerCanDash = !ControllerCanDash;
        public void ChangeControllerCanDodgeRoll() => ControllerCanDodgeRoll = !ControllerCanDodgeRoll;
        public void ChangeControllerCanGrab() => ControllerCanGrab = !ControllerCanGrab;
        public void ChangeControllerCanWallSlide() => ControllerCanWallSlide = !ControllerCanWallSlide;
        public void ChangeControllerCanLedgeClimb() => ControllerCanLedgeClimb = !ControllerCanLedgeClimb;
    }
}
