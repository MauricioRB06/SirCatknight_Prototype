// The purpose of this script is:
/* Controlling the player character */

/* Documentation and References:
 * 
 * Awake: https://docs.unity3d.com/ScriptReference/MonoBehaviour.Awake.html
 * Start: https://docs.unity3d.com/ScriptReference/MonoBehaviour.Start.html
 * Update: https://docs.unity3d.com/ScriptReference/MonoBehaviour.Update.html
 * Fixed Update: https://docs.unity3d.com/ScriptReference/MonoBehaviour.FixedUpdate.html
 * PlayerPrefs: https://docs.unity3d.com/ScriptReference/PlayerPrefs.html
 * SerializeField: https://docs.unity3d.com/ScriptReference/SerializeField.html
 * 
 */

using _Development.Scripts.Mauricio;
using Interfaces;
using Player.Data;
using Player.Input;
using Player.Inventory;
using Player.PlayerStates;
using Player.PlayerStates.PlayerAbilityState;
using Player.PlayerStates.PlayerGroundedState;
using Player.PlayerStates.PlayerTouchingWallState;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour, IDamageableObject
    {
        public static PlayerController Instance;
        
        // We use it to store the reference to the player's data file
        [SerializeField] private PlayerData playerData;
        
        // We use them to store the state machine for the player
        private StateMachine.StateMachine StateMachine { get; set; }
        
        // We use them to store player states
        public PlayerIdleState IdleState { get; private set; }
        public PlayerSleepState SleepState { get; private set; }
        public PlayerWalkState WalkState { get; private set; }
        public PlayerRunState RunState { get; private set; }
        public PlayerJumpState JumpState { get; private set; }
        public PlayerDodgeRoll DodgeRoll { get; private set; }
        public PlayerAttackState PrimaryAttackState { get; private set; }
        public PlayerAttackState SecondaryAttackState { get; private set; }
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
        
        // We use them to access the player's components
        public Core.Core Core { get; private set; }
        public Animator PlayerAnimator { get; private set; }
        public PlayerInputHandler InputHandler { get; private set; }
        private Rigidbody2D PlayerRigidBody2D { get; set; }
        public Transform DashDirectionIndicator { get; private set; }
        private CapsuleCollider2D PlayerCollider { get; set; }
        public PlayerInventory PlayerInventory { get; private set; }
        public PlayerHealth playerHealth { get; private set; }
        public PlayerSounds playerSounds { get; private set; }

        // We use it to set a new heightCollider for the player depending on whether he is crouched or not
        private Vector2 _updateCollider;

        // We use them to store the position of the checkpoint reached
        private float _checkPointPositionX, _checkPointPositionY;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            
            Core = GetComponentInChildren<Core.Core>();
            
            StateMachine = new StateMachine.StateMachine();
            
            IdleState = new PlayerIdleState(this, StateMachine, playerData, "Idle");
            SleepState = new PlayerSleepState(this, StateMachine, playerData, "Sleep");
            WalkState = new PlayerWalkState(this, StateMachine, playerData, "Walk");
            RunState = new PlayerRunState(this, StateMachine, playerData, "Run");
            JumpState = new PlayerJumpState(this, StateMachine, playerData, "InAir");
            DodgeRoll = new PlayerDodgeRoll(this, StateMachine, playerData,"DodgeRoll");
            PrimaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "Attack");
            SecondaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "Attack");
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
            PlayerAnimator = GetComponent<Animator>();
            InputHandler = GetComponent<PlayerInputHandler>();
            PlayerRigidBody2D = GetComponent<Rigidbody2D>();
            DashDirectionIndicator = transform.Find("DashDirectionIndicator");
            PlayerCollider = GetComponent<CapsuleCollider2D>();
            PlayerInventory = GetComponent<PlayerInventory>();
            playerHealth = GetComponent<PlayerHealth>();
            playerSounds = GetComponent<PlayerSounds>();

            //
            PrimaryAttackState.SetWeapon(PlayerInventory.weapons[(int)CombatInputs.PrimaryAttackInput]);
            
            StateMachine.Initialize(IdleState); 
        }

        private void Update()
        {
            Core.LogicUpdate();
            StateMachine.CurrentState.LogicUpdate();
        }

        private void FixedUpdate() { StateMachine.CurrentState.PhysicsUpdate(); }
        
        public void Damage(float amount)
        {
            Debug.Log($"Ohh no! { amount }");
            playerHealth.TakeDamage(amount);
        }
        
        public bool CheckIfPlayerSleep() { return StateMachine.CurrentState == SleepState; }
        
        // We use it to change the heightCollider and the offsetCollider of the character
        public void SetColliderHeight(float heightCollider)
        {
            var centerCollider = PlayerCollider.offset;
            _updateCollider.Set(PlayerCollider.size.x, heightCollider);

            centerCollider.y += (heightCollider - PlayerCollider.size.y) / 2;

            PlayerCollider.size = _updateCollider;
            PlayerCollider.offset = centerCollider;
        }

        public static void Die(string typeOfDeath)
        {
            Debug.Log($"Death by: {typeOfDeath}");
            GameManager.Instance.GameOver();
        }
        
        // We use it to trigger events in the middle of an animation ( We call it inside the Animator component )
        private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
        
        // We use it to trigger events at the end of an animation ( We call it inside the Animator component )
        private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
        
    }
}
