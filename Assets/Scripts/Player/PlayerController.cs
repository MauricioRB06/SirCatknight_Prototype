
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

using _Development.Scripts.Mauricio;
using Interfaces;
using Player.Data;
using Player.Input;
using Player.Inventory;
using Player.PlayerStates;
using Player.PlayerStates.BaseStates;
using Player.PlayerStates.PlayerAbilityState;
using Player.PlayerStates.PlayerGroundedState;
using Player.PlayerStates.PlayerTouchingWallState;
using StateMachine;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance;
        
        // We use it to store the reference to the player's data file
        [SerializeField] private DataPlayerController dataPlayerController;
        
        // To stored the state machine for the player.
        private PlayerStateMachine PlayerStateMachine { get; set; }

        public AnimatorController animatorA;
        public AnimatorController animatorB;
        
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
            
            DodgeRoll = new PlayerDodgeRoll(this, PlayerStateMachine,
                                                        dataPlayerController,"DodgeRoll");
            
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
            DashDirectionIndicator = transform.Find("DashDirectionIndicator");
            PlayerCollider = GetComponent<CapsuleCollider2D>();
            PlayerInventory = GetComponent<PlayerInventory>();
            playerHealth = GetComponent<PlayerHealth>();
            playerSounds = GetComponent<PlayerSounds>();

            //
            PrimaryAttackState.SetWeapon(PlayerInventory.weapons[(int)CombatInputs.PrimaryAttackInput]);
            
            PlayerStateMachine.Initialize(IdleState); 
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

        public static void Die(string typeOfDeath)
        {
            Debug.Log($"Death by: {typeOfDeath}");
            GameManager.Instance.GameOver();
        }
        
        // We use it to trigger events in the middle of an animation ( We call it inside the Animator component )
        private void AnimationTrigger() => PlayerStateMachine.CurrentState.AnimationTrigger();
        
        // We use it to trigger events at the end of an animation ( We call it inside the Animator component )
        private void AnimationFinishTrigger() => PlayerStateMachine.CurrentState.AnimationFinishTrigger();
        
        [ContextMenu("ChangeAnimator")]
        public void ChangeAnimator()
        {
            if (PlayerAnimator.runtimeAnimatorController == animatorA)
            {
                PlayerAnimator.runtimeAnimatorController = animatorB;
            }
            else
            {
                PlayerAnimator.runtimeAnimatorController = animatorA;
            }
        }
        
    }
}
