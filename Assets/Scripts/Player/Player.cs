using Player.Data;
using Player.Input;
using Player.PlayerStates;
using Player.PlayerStates.PlayerAbilityState;
using Player.PlayerStates.PlayerGroundedState;
using Player.PlayerStates.PlayerTouchingWallState;
using Player.StateMachine;
using UnityEngine;

/* Documentation:
 * 
 * Awake: https://docs.unity3d.com/ScriptReference/MonoBehaviour.Awake.html
 * Start: https://docs.unity3d.com/ScriptReference/MonoBehaviour.Start.html
 * Update: https://docs.unity3d.com/ScriptReference/MonoBehaviour.Update.html
 * Fixed Update: https://docs.unity3d.com/ScriptReference/MonoBehaviour.FixedUpdate.html
 * PlayerPrefs: https://docs.unity3d.com/ScriptReference/PlayerPrefs.html
 * SerializeField: https://docs.unity3d.com/ScriptReference/SerializeField.html
 * 
 */

namespace Player
{
    public class Player : MonoBehaviour
    {
        // We use it to store access to the player's data file
        [SerializeField]
        private PlayerData playerData;
        
        // We use it to store player state machine
        private PlayerStateMachine StateMachine { get; set; }
        
        // We use it to store player statuses
        public PlayerIdleState IdleState { get; private set; }
        public PlayerSleepState SleepState { get; private set; }
        public PlayerWalkState WalkState { get; private set; }
        public PlayerRunState RunState { get; private set; }
        public PlayerJumpState JumpState { get; private set; }
        public PlayerInAirState InAirState { get; private set; }
        public PlayerLandState LandState { get; private set; }
        public PlayerWallSlideState WallSlideState { get; private set; }
        public PlayerWallGrabState WallGrabState { get; private set; }
        public PlayerWallClimbState WallClimbState { get; private set; }
        public PlayerWallJumpState WallJumpState { get; private set; }
        public PlayerLedgeClimbState LedgeClimbState { get; private set; }
        public PlayerDashState DashState { get; private set; }
        public PlayerCrouchIdleState CrouchIdleState { get; private set; }
        public PlayerCrouchMoveState CrouchMoveState { get; private set; }
        
        // We use them to access the player components
        public Animator PlayerAnimator { get; private set; }
        public PlayerInputHandler InputHandler { get; private set; }
        private Rigidbody2D PlayerRigidBody2D { get; set; }
        public Transform DashDirectionIndicator { get; private set; }
        private BoxCollider2D PlayerCollider { get; set; }

        // We use them to perform player status checks
        [SerializeField]
        private Transform groundCheck;
        [SerializeField]
        private Transform wallCheck;
        [SerializeField]
        private Transform ledgeCheck;
        [SerializeField]
        private Transform ceilingCheck;

        // We use it to not call the RigidBody2D and ask for the speed in 'Y' at all times
        public Vector2 CurrentVelocity { get; private set; }
        public int FacingDirection { get; private set; }    
        
        // We use it so we don't have to create a new vector every time the character moves
        private Vector2 _newVelocity;
        
        // We use it to return to store the position of the corners that are detected
        private Vector2 _detectedCornerPosition;
        
        // We use them to set a new heightCollider of the colliding player, depending on whether he is crouching or not
        private Vector2 _updateCollider;

        // Here we are going to save the position of the checkpoint reached
        private float _checkPointPositionX, _checkPointPositionY;
        
        private void Awake()
        {
            StateMachine = new PlayerStateMachine();
            
            IdleState = new PlayerIdleState(this, StateMachine, playerData, "Idle");
            SleepState = new PlayerSleepState(this, StateMachine, playerData, "Sleep");
            WalkState = new PlayerWalkState(this, StateMachine, playerData, "Walk");
            RunState = new PlayerRunState(this, StateMachine, playerData, "Run");
            JumpState = new PlayerJumpState(this, StateMachine, playerData, "InAir");
            InAirState = new PlayerInAirState(this, StateMachine, playerData, "InAir");
            LandState = new PlayerLandState(this, StateMachine, playerData, "Landing");
            WallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, "WallSlide");
            WallGrabState = new PlayerWallGrabState(this, StateMachine, playerData, "WallGrab");
            WallClimbState = new PlayerWallClimbState(this, StateMachine, playerData, "WallClimb");
            WallJumpState = new PlayerWallJumpState(this, StateMachine, playerData, "InAir");
            LedgeClimbState = new PlayerLedgeClimbState(this, StateMachine, playerData, "LedgeClimb");
            DashState = new PlayerDashState(this, StateMachine, playerData, "Dash");
            CrouchIdleState = new PlayerCrouchIdleState(this, StateMachine, playerData, "CrouchIdle");
            CrouchMoveState = new PlayerCrouchMoveState(this, StateMachine, playerData, "CrouchMove");
        }

        private void Start()
        {
            /*
             
             Don't Delete :v
             
             if (PlayerPrefs.GetFloat("_checkPointPositionX") != 0)
            {
                transform.position = (new Vector2(PlayerPrefs.GetFloat("_checkPointPositionX"),
                    PlayerPrefs.GetFloat("_checkPointPositionY")));
                    
            }*/

            PlayerAnimator = GetComponent<Animator>();
            InputHandler = GetComponent<PlayerInputHandler>();
            PlayerRigidBody2D = GetComponent<Rigidbody2D>();
            DashDirectionIndicator = transform.Find("DashDirectionIndicator");
            PlayerCollider = GetComponent<BoxCollider2D>();
            
            // The character always starts looking to the right
            FacingDirection = 1;    
            
            // We initialize the state machine, with the status that the player will have at the beginning of the game
            StateMachine.Initialize(IdleState); 
        }

        private void Update()
        {
            CurrentVelocity = PlayerRigidBody2D.velocity;
            StateMachine.CurrentState.LogicUpdate();
        }

        private void FixedUpdate()
        {
            StateMachine.CurrentState.PhysicsUpdate();
        }
        
        // we use it to set the player's speed to 0
        public void SetVelocityZero()
        {
            PlayerRigidBody2D.velocity = Vector2.zero;
            CurrentVelocity = Vector2.zero;
        }
        
        // We use it to apply a speed in a wall jump
        public void SetVelocity(float velocity, Vector2 angle, int direction)
        {
            angle.Normalize();
            _newVelocity.Set(angle.x * velocity * direction, angle.y * velocity);
            PlayerRigidBody2D.velocity = _newVelocity;
            CurrentVelocity = _newVelocity;
        }
        
        // We use it to apply a speed in the DashState
        public void SetVelocity(float velocity, Vector2 direction)
        {
            _newVelocity = direction * velocity;
            PlayerRigidBody2D.velocity = _newVelocity;
            CurrentVelocity = _newVelocity;
        }
        
        // We use it to apply a moving force on the X axis
        public void SetVelocityX(float velocity)
        {
            _newVelocity.Set(velocity, CurrentVelocity.y);
            PlayerRigidBody2D.velocity = _newVelocity;
            CurrentVelocity = _newVelocity;
        }
        
        // We use it to apply a moving force on the Y axis
        public void SetVelocityY(float velocity)
        {
            _newVelocity.Set(CurrentVelocity.x, velocity);
            PlayerRigidBody2D.velocity = _newVelocity;
            CurrentVelocity = _newVelocity;
        }

        // We use it to detect if we are touching a ceiling 
        public bool CheckForCeiling()
        {
            return Physics2D.OverlapCircle(ceilingCheck.position, playerData.ceilingCheckRadius,
                playerData.layerGroundWalls);
        }
        
        // We use it to check whether or not we are touching the ground
        public bool CheckIfGrounded()
        {
            var groundCheckPosition = groundCheck.position;
            return Physics2D.OverlapCircle(groundCheckPosition, playerData.groundCheckRadius,
                playerData.layerGroundWalls);
        }
        
        // We use it to check whether or not we are touching a wall
        public bool CheckIfTouchingWall()
        {
            return Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection,
                playerData.wallCheckDistance, playerData.layerGroundWalls);
        }
        
        // We use it to check whether or not we are touching a wall behind the player
        public bool CheckIfTouchingWallBack()
        {
            return Physics2D.Raycast(wallCheck.position, Vector2.right * -FacingDirection,
                playerData.wallCheckDistance, playerData.layerGroundWalls);
        }
        
        // We use it to detect if it is touching a ledge
        public bool CheckIfTouchingLedge()
        {
            return Physics2D.Raycast(ledgeCheck.position, Vector2.right * FacingDirection,
                playerData.wallCheckDistance, playerData.layerGroundWalls);
        }

        // Check if we should rotate the character
        public void CheckIfShouldFlip(int xInput)
        {
            if(xInput != 0 && xInput != FacingDirection)
            {
                Flip();
            }
        }

        public bool CheckIfPlayerSleep()
        {
            return StateMachine.CurrentState == SleepState;
        }
        
        // We use it to change the heightCollider and center of the character's collider
        public void SetColliderHeight(float heightCollider)
        {
            var centerCollider = PlayerCollider.offset;
            _updateCollider.Set(PlayerCollider.size.x, heightCollider);

            centerCollider.y += (heightCollider - PlayerCollider.size.y) / 2;

            PlayerCollider.size = _updateCollider;
            PlayerCollider.offset = centerCollider;
        }
        
        // We use it to determine the position of the detected corner
        public Vector2 DetermineCornerPosition()
        {
            var wallCheckPosition = wallCheck.position;
            var ledgeCheckPosition = ledgeCheck.position;
            
            // First we detect the distance from the wall
            var xWallHit = Physics2D.Raycast(wallCheckPosition, Vector2.right * FacingDirection,
                playerData.wallCheckDistance, playerData.layerGroundWalls);
            var xWallDistance = xWallHit.distance;
            
            // We save that distance from the wall to use to check the ground clearance
            _detectedCornerPosition.Set((xWallDistance + 0.015f) * FacingDirection, 0f);
            
            // Determine the distance from the ground
            var yFloorHit = Physics2D.Raycast(ledgeCheckPosition + (Vector3)(_detectedCornerPosition),
                Vector2.down, ledgeCheckPosition.y - wallCheckPosition.y + 0.015f,playerData.layerGroundWalls);
            var yFloorDistance = yFloorHit.distance;
            
            // Finally we get the exact position of the corner
            _detectedCornerPosition.Set(wallCheckPosition.x + (xWallDistance * FacingDirection),
                ledgeCheckPosition.y - yFloorDistance);
            
            return _detectedCornerPosition;
        }

        private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

        private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

        // Rotate the character according to the direction of movement
        private void Flip()
        {
            FacingDirection *= -1;
            transform.Rotate(0.0f, 180.0f, 0.0f);
        }
        
        /*
        public static void ReachedCheckpoint(float x, float y)
        {
            PlayerPrefs.SetFloat("checkPointPositionX", x);
            PlayerPrefs.SetFloat("checkPointPositionY", y);
        }*/
    }
}
