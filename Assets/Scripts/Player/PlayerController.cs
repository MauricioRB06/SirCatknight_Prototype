
// Copyright (c) 2021 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
//
//  The Purpose Of This Script Is:
//
//  Controlling the player character.
// 
//  Documentation and References:
//
//  Awake: https://docs.unity3d.com/ScriptReference/MonoBehaviour.Awake.html
//  Start: https://docs.unity3d.com/ScriptReference/MonoBehaviour.Start.html
//  Update: https://docs.unity3d.com/ScriptReference/MonoBehaviour.Update.html
//  Fixed Update: https://docs.unity3d.com/ScriptReference/MonoBehaviour.FixedUpdate.html
//  PlayerPrefs: https://docs.unity3d.com/ScriptReference/PlayerPrefs.html
//  SerializeField: https://docs.unity3d.com/ScriptReference/SerializeField.html
//  C# Protected: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/protected
//  C# Read only: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/readonly
//  C# Polymorphism: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/virtual
//  C# Polymorphism: https://www.youtube.com/watch?v=XzKL94OMDV4&list=PLU8oAlHdN5BmpIQGDSHo5e1r4ZYWQ8m4B&index=46 [ Spanish ]

using System;
using _Development.Scripts.Mauricio;
using Player.Data;
using Player.Input;
using Player.Inventory;
using Player.PlayerStates.BaseStates;
using Player.PlayerStates.PlayerAbilityState;
using Player.PlayerStates.PlayerGroundedState;
using Player.PlayerStates.PlayerTouchingWallState;
using StateMachine;
using UnityEditor.Animations;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance;
        
        // We use it to store the reference to the player's data file
        [SerializeField] private DataPlayerController dataPlayerController;
        public DataPlayerController DataPlayerController => dataPlayerController;
        
        
        [SerializeField] private AnimatorController playerAnimatorBase;
        
        
        [SerializeField] private Transform interactPosition;
        public Transform InteractPosition => interactPosition;
        
        
        [SerializeField] private Transform dashIndicatorPosition;
        
        [SerializeField] private Transform jumpDustPosition;
        [SerializeField] private Transform wallSlideDustPosition;
        
        // To stored the state machine for the player.
        private PlayerStateMachine PlayerStateMachine { get; set; }

        
        public ParticleSystem dustParticles;
        
        // We use them to store player states
        public PlayerIdleState IdleState { get; private set; }
        public PlayerSleepState SleepState { get; private set; }
        public PlayerWalkState WalkState { get; private set; }
        public PlayerRunState RunState { get; private set; }
        public PlayerJumpState JumpState { get; private set; }
        public PlayerDodgeRollState DodgeRollState { get; private set; }
        public PlayerInteractState InteractState { get; private set; }
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
        public PlayerHealth PlayerHealth { get; private set; }
        public PlayerSounds PlayerSounds { get; private set; }

        // We use it to set a new heightCollider for the player depending on whether he is crouched or not
        private Vector2 _updateCollider;

        // We use them to store the position of the checkpoint reached
        private float _checkPointPositionX, _checkPointPositionY;
        
        private void Awake()
        {
            // 
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            
            // 
            Core = GetComponentInChildren<Core.Core>();
            PlayerStateMachine = new PlayerStateMachine();
            
            // 
            IdleState = new PlayerIdleState(this, PlayerStateMachine,
                                                        dataPlayerController, "Idle");
            
            SleepState = new PlayerSleepState(this, PlayerStateMachine,
                                                        dataPlayerController, "Sleep");
            
            WalkState = new PlayerWalkState(this, PlayerStateMachine,
                                                        dataPlayerController, "Walk");
            
            RunState = new PlayerRunState(this, PlayerStateMachine,
                                                        dataPlayerController, "Run");
            
            JumpState = new PlayerJumpState(this, PlayerStateMachine,
                                                        dataPlayerController, "InAir");
            
            DodgeRollState = new PlayerDodgeRollState(this, PlayerStateMachine,
                                                        dataPlayerController,"DodgeRoll");
            
            InteractState = new PlayerInteractState(this, PlayerStateMachine,
                                                        dataPlayerController,"Interact");
            
            PrimaryAttackState = new PlayerAttackState(this, PlayerStateMachine,
                                                        dataPlayerController, "Attack");
            
            SecondaryAttackState = new PlayerAttackState(this, PlayerStateMachine,
                                                        dataPlayerController, "Attack");
            
            InAirState = new PlayerInAirState(this, PlayerStateMachine,
                                                        dataPlayerController, "InAir");
            
            LandState = new PlayerLandState(this, PlayerStateMachine,
                                                        dataPlayerController, "Landing");
            
            WallSlideState = new PlayerWallSlideState(this, PlayerStateMachine,
                                                        dataPlayerController, "WallSlide");
            
            WallGrabState = new PlayerWallGrabState(this, PlayerStateMachine,
                                                        dataPlayerController, "WallGrab");
            
            WallClimbState = new PlayerWallClimbState(this, PlayerStateMachine,
                                                        dataPlayerController, "WallClimb");
            
            WallJumpState = new PlayerWallJumpState(this, PlayerStateMachine,
                                                        dataPlayerController, "InAir");
            
            LedgeClimbState = new PlayerLedgeClimbState(this, PlayerStateMachine,
                                                        dataPlayerController, "LedgeClimb");
            
            DashState = new PlayerDashState(this, PlayerStateMachine,
                                                        dataPlayerController, "Dash");
            
            CrouchIdleState = new PlayerCrouchIdleState(this, PlayerStateMachine,
                                                        dataPlayerController, "CrouchIdle");
            
            CrouchMoveState = new PlayerCrouchMoveState(this, PlayerStateMachine,
                                                        dataPlayerController, "CrouchMove");
        }
        
        // 
        private void Start()
        {
            PlayerAnimator = GetComponent<Animator>();
            InputHandler = GetComponent<PlayerInputHandler>();
            PlayerRigidBody2D = GetComponent<Rigidbody2D>();
            DashDirectionIndicator = dashIndicatorPosition;
            PlayerCollider = GetComponent<CapsuleCollider2D>();
            PlayerInventory = GetComponent<PlayerInventory>();
            PlayerHealth = GetComponent<PlayerHealth>();
            PlayerSounds = GetComponent<PlayerSounds>();

            //
            PrimaryAttackState.SetWeapon(PlayerInventory.weapons[(int)CombatInputs.PrimaryAttackInput]);
            
            PlayerStateMachine.Initialize(SleepState); 
        }
        
        private void Update()
        {
            Core.LogicUpdate();
            PlayerStateMachine.CurrentState.LogicUpdate();
        }
        
        // 
        private void FixedUpdate()
        {
            PlayerStateMachine.CurrentState.PhysicsUpdate();
        }
        
        // 
        public bool CheckIfPlayerSleep()
        {
            return PlayerStateMachine.CurrentState == SleepState;
        }
        
        // We use it to change the heightCollider and the offsetCollider of the character
        public void SetColliderHeight(float heightCollider)
        {
            var centerCollider = PlayerCollider.offset;
            _updateCollider.Set(PlayerCollider.size.x, heightCollider);

            centerCollider.y += (heightCollider - PlayerCollider.size.y) / 2;

            PlayerCollider.size = _updateCollider;
            PlayerCollider.offset = centerCollider;
        }
        
        // 
        public static void Die(string typeOfDeath)
        {
            Debug.Log($"Death by: {typeOfDeath}");
            GameManager.Instance.GameOver();
        }
        
        // We use it to trigger events in the middle of an animation ( We call it inside the Animator component )
        private void AnimationTrigger() => PlayerStateMachine.CurrentState.AnimationTrigger();
        
        // We use it to trigger events at the end of an animation ( We call it inside the Animator component )
        private void AnimationFinishTrigger() => PlayerStateMachine.CurrentState.AnimationFinishTrigger();
        
        // 
        public void ChangeAnimatorWeapon(Animator weaponAnimator)
        {
            PlayerAnimator.runtimeAnimatorController = playerAnimatorBase;
        }
        
        // 
        public void ResetAnimator() => PlayerAnimator.runtimeAnimatorController = playerAnimatorBase;
        
        // 
        public void JumpDust()
        {
            Instantiate(dustParticles, jumpDustPosition.position, Quaternion.identity);
        }
        
        // 
        public void WallSlideDust()
        {
            Instantiate(dustParticles, wallSlideDustPosition.position, Quaternion.Euler(0,0,0));
        }
        
        // Delete this in final build, only for testing an development
        public void OnDrawGizmos()
        {
            // To view the interaction range.
            Gizmos.DrawWireSphere(InteractPosition.position, DataPlayerController.interactionRadius);

            if (Core == null) return;
            
            // To view wallCheck distance
            Gizmos.color = Color.red;
            Gizmos.DrawLine(Core.CollisionSenses.WallCheck.position,
                Core.CollisionSenses.WallCheck.position + (Vector3)(Vector2.right * Core.Movement.FacingDirection
                    * Core.CollisionSenses.WallCheckDistance));
            
            // To view groundCheck radius
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(Core.CollisionSenses.GroundCheck.position,Core.CollisionSenses.GroundCheckRadius);
            
            // To view ceilingCheck distance
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(Core.CollisionSenses.CeilingCheck.position,
                Core.CollisionSenses.CeilingCheck.position + (Vector3)(Vector2.up * Core.Movement.FacingDirection
                    * Core.CollisionSenses.WallCheckDistance));
            
            // To view ledgeCheck distance
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(Core.CollisionSenses.LedgeCheckHorizontal.position,
                Core.CollisionSenses.LedgeCheckHorizontal.position + (Vector3)(Vector2.right
                 * Core.Movement.FacingDirection * Core.CollisionSenses.WallCheckDistance));
            
        }
    }
}
