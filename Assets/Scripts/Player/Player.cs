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
        public PlayerMoveState MoveState { get; private set; }
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
        public Rigidbody2D PlayerRigidBody2D { get; private set; }
        public Transform DashDirectionIndicator { get; private set; }
        private BoxCollider2D MovementCollider { get; set; }

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

        // Here we are going to save the position of the checkpoint reached
        private float _checkPointPositionX, _checkPointPositionY;
        
        private void Awake()
        {
            StateMachine = new PlayerStateMachine();
            IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
            MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
            JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
            InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
            LandState = new PlayerLandState(this, StateMachine, playerData, "land");
            WallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, "wallSlide");
            WallGrabState = new PlayerWallGrabState(this, StateMachine, playerData, "wallGrab");
            WallClimbState = new PlayerWallClimbState(this, StateMachine, playerData, "wallClimb");
            WallJumpState = new PlayerWallJumpState(this, StateMachine, playerData, "inAir");
            LedgeClimbState = new PlayerLedgeClimbState(this, StateMachine, playerData, "ledgeClimb");
            DashState = new PlayerDashState(this, StateMachine, playerData, "Dash");
            CrouchIdleState = new PlayerCrouchIdleState(this, StateMachine, playerData, "crouchIdle");
            CrouchMoveState = new PlayerCrouchMoveState(this, StateMachine, playerData, "crouchMove");
        }

        private void Start()
        {
            if (PlayerPrefs.GetFloat("_checkPointPositionX") != 0)
            {
                transform.position = (new Vector2(PlayerPrefs.GetFloat("_checkPointPositionX"),
                    PlayerPrefs.GetFloat("_checkPointPositionY")));
            }

            PlayerAnimator = GetComponent<Animator>();
            InputHandler = GetComponent<PlayerInputHandler>();
            PlayerRigidBody2D = GetComponent<Rigidbody2D>();
            DashDirectionIndicator = transform.Find("DashDirectionIndicator");
            MovementCollider = GetComponent<BoxCollider2D>();
            
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
        
        //
        public void SetVelocityZero()
        {
            PlayerRigidBody2D.velocity = Vector2.zero;
            CurrentVelocity = Vector2.zero;
        }
        
        //
        public void SetVelocity(float velocity, Vector2 angle, int direction)
        {
            angle.Normalize();
            _newVelocity.Set(angle.x * velocity * direction, angle.y * velocity);
            PlayerRigidBody2D.velocity = _newVelocity;
            CurrentVelocity = _newVelocity;
        }
        
        //
        public void SetVelocity(float velocity, Vector2 direction)
        {
            _newVelocity = direction * velocity;
            PlayerRigidBody2D.velocity = _newVelocity;
            CurrentVelocity = _newVelocity;
        }
        
        //
        public void SetVelocityX(float velocity)
        {
            _newVelocity.Set(velocity, CurrentVelocity.y);
            PlayerRigidBody2D.velocity = _newVelocity;
            CurrentVelocity = _newVelocity;
        }
        
        //
        public void SetVelocityY(float velocity)
        {
            _newVelocity.Set(CurrentVelocity.x, velocity);
            PlayerRigidBody2D.velocity = _newVelocity;
            CurrentVelocity = _newVelocity;
        }

        // 
        public bool CheckForCeiling()
        {
            return Physics2D.OverlapCircle(ceilingCheck.position, playerData.groundCheckRadius,
                playerData.layerGround);
        }
        
        // 
        public bool CheckIfGrounded()
        {
            var groundCheckPosition = groundCheck.position;
            return Physics2D.OverlapCircle(groundCheckPosition, playerData.groundCheckRadius,
                playerData.layerGround);
        }
        
        // 
        public bool CheckIfTouchingWall()
        {
            return Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection,
                playerData.wallCheckDistance, playerData.layerGround);
        }
        
        // 
        public bool CheckIfTouchingLedge()
        {
            return Physics2D.Raycast(ledgeCheck.position, Vector2.right * FacingDirection,
                playerData.wallCheckDistance, playerData.layerGround);
        }
        
        // 
        public bool CheckIfTouchingWallBack()
        {
            return Physics2D.Raycast(wallCheck.position, Vector2.right * -FacingDirection,
                playerData.wallCheckDistance, playerData.layerGround);
        }

        // Check if we should rotate the character
        public void CheckIfShouldFlip(int xInput)
        {
            if(xInput != 0 && xInput != FacingDirection)
            {
                Flip();
            }
        }
        
        // 
        public void SetColliderHeight(float height)
        {
            var center = MovementCollider.offset;
            _newVelocity.Set(MovementCollider.size.x, height);

            center.y += (height - MovementCollider.size.y) / 2;

            MovementCollider.size = _newVelocity;
            MovementCollider.offset = center;
        }
        
        // 
        public Vector2 DetermineCornerPosition()
        {
            var wallPosition = wallCheck.position;
            RaycastHit2D xHit = Physics2D.Raycast(wallPosition, Vector2.right * FacingDirection,
                playerData.wallCheckDistance, playerData.layerGround);
            var xDist = xHit.distance;
            _newVelocity.Set((xDist + 0.015f) * FacingDirection, 0f);
            var ledgePosition = ledgeCheck.position;
            RaycastHit2D yHit = Physics2D.Raycast(ledgePosition + (Vector3)(_newVelocity),
                Vector2.down, ledgePosition.y - wallPosition.y + 0.015f, playerData.layerGround);
            var yDist = yHit.distance;

            _newVelocity.Set(wallPosition.x + (xDist * FacingDirection), ledgePosition.y - yDist);
            return _newVelocity;
        }

        private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

        private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

        // Rotate the character according to the direction of movement
        private void Flip()
        {
            FacingDirection *= -1;
            transform.Rotate(0.0f, 180.0f, 0.0f);
        }
        
        //
        public static void ReachedCheckpoint(float x, float y)
        {
            PlayerPrefs.SetFloat("checkPointPositionX", x);
            PlayerPrefs.SetFloat("checkPointPositionY", y);
        }
    }
}
