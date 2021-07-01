using Player.Data;
using Player.Input;
using Player.Inventory;
using Player.PlayerStates;
using Player.PlayerStates.PlayerAbilityState;
using Player.PlayerStates.PlayerGroundedState;
using Player.PlayerStates.PlayerTouchingWallState;
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
        
        // We use it to store player weaponState machine
        private global::StateMachine.StateMachine StateMachine { get; set; }
        
        // We use it to store player statuses
        public EntityIdleState IdleState { get; private set; }
        public EntitySleepState SleepState { get; private set; }
        public EntityWalkState WalkState { get; private set; }
        public EntityRunState RunState { get; private set; }
        public EntityJumpState JumpState { get; private set; }
        public EntityDodgeRoll DodgeRoll { get; private set; }
        public EntityAttackState PrimaryAttackState { get; private set; }
        public EntityAttackState SecondaryAttackState { get; private set; }
        public EntityInAirState InAirState { get; private set; }
        public EntityLandState LandState { get; private set; }
        public EntityWallSlideState WallSlideState { get; private set; }
        public EntityWallGrabState WallGrabState { get; private set; }
        public EntityWallClimbState WallClimbState { get; private set; }
        public EntityWallJumpState WallJumpState { get; private set; }
        public EntityLedgeClimbState LedgeClimbState { get; private set; }
        public EntityDashState DashState { get; private set; }
        public EntityCrouchIdleState CrouchIdleState { get; private set; }
        public EntityCrouchMoveState CrouchMoveState { get; private set; }
        
        // We use them to access the player components
        public Core.Core Core { get; private set; }
        public Animator PlayerAnimator { get; private set; }
        public PlayerInputHandler InputHandler { get; private set; }
        private Rigidbody2D PlayerRigidBody2D { get; set; }
        public Transform DashDirectionIndicator { get; private set; }
        private CapsuleCollider2D PlayerCollider { get; set; }
        public PlayerInventory PlayerInventory { get; private set; }

        // We use it so we don't have to create a new vector every time the character moves
        private Vector2 _newVelocity;

        // We use them to set a new heightCollider of the colliding player, depending on whether he is crouching or not
        private Vector2 _updateCollider;

        // Here we are going to save the position of the checkpoint reached
        private float _checkPointPositionX, _checkPointPositionY;
        
        private void Awake()
        {
            Core = GetComponentInChildren<Core.Core>();
            
            StateMachine = new global::StateMachine.StateMachine();
            
            IdleState = new EntityIdleState(this, StateMachine, playerData, "Idle");
            SleepState = new EntitySleepState(this, StateMachine, playerData, "Sleep");
            WalkState = new EntityWalkState(this, StateMachine, playerData, "Walk");
            RunState = new EntityRunState(this, StateMachine, playerData, "Run");
            JumpState = new EntityJumpState(this, StateMachine, playerData, "InAir");
            DodgeRoll = new EntityDodgeRoll(this, StateMachine, playerData,"DodgeRoll");
            PrimaryAttackState = new EntityAttackState(this, StateMachine, playerData, "Attack");
            SecondaryAttackState = new EntityAttackState(this, StateMachine, playerData, "Attack");
            InAirState = new EntityInAirState(this, StateMachine, playerData, "InAir");
            LandState = new EntityLandState(this, StateMachine, playerData, "Landing");
            WallSlideState = new EntityWallSlideState(this, StateMachine, playerData, "WallSlide");
            WallGrabState = new EntityWallGrabState(this, StateMachine, playerData, "WallGrab");
            WallClimbState = new EntityWallClimbState(this, StateMachine, playerData, "WallClimb");
            WallJumpState = new EntityWallJumpState(this, StateMachine, playerData, "InAir");
            LedgeClimbState = new EntityLedgeClimbState(this, StateMachine, playerData, "LedgeClimb");
            DashState = new EntityDashState(this, StateMachine, playerData, "Dash");
            CrouchIdleState = new EntityCrouchIdleState(this, StateMachine, playerData, "CrouchIdle");
            CrouchMoveState = new EntityCrouchMoveState(this, StateMachine, playerData, "CrouchMove");
        }

        private void Start()
        {
            PlayerAnimator = GetComponent<Animator>();
            InputHandler = GetComponent<PlayerInputHandler>();
            PlayerRigidBody2D = GetComponent<Rigidbody2D>();
            DashDirectionIndicator = transform.Find("DashDirectionIndicator");
            PlayerCollider = GetComponent<CapsuleCollider2D>();
            PlayerInventory = GetComponent<PlayerInventory>();

            //
            PrimaryAttackState.SetWeapon(PlayerInventory.weapons[(int)CombatInputs.Primary]);
            
            /* We initialize the weaponState machine, with the status that the player will have at
             the beginning of the game */
            StateMachine.Initialize(IdleState); 
        }

        private void Update()
        {
            Core.LogicUpdate();
            StateMachine.CurrentState.LogicUpdate();
        }

        private void FixedUpdate()
        {
            StateMachine.CurrentState.PhysicsUpdate();
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
        
        private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

        private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
        
    }
}