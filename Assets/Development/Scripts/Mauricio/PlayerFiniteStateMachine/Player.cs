using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region State Variables

    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMovetState MovetState { get; private set; }

    [SerializeField] private PlayerData playerData;

    #endregion

    #region Components

    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D PlayerRigidbody2D { get; private set; }

    #endregion

    #region Other Variables

    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection { get; private set; }

    private Vector2 workspace;

    #endregion

    #region Unity Callback Functions
    private void Awake()
    {
        stateMachine = new PlayerStateMachine(); // Con esto cada vez que inicie el juego tendremos una máquina de estado para nuestro jugador
        IdleState = new PlayerIdleState(this, stateMachine, playerData, "idle");
        MovetState = new PlayerMovetState(this, stateMachine, playerData, "move");
    }

    private void Start()
    {
        Anim = GetComponent<Animator>(); // Obtenemos una referencia de nuestro Animator
        InputHandler = GetComponent<PlayerInputHandler>();
        PlayerRigidbody2D = GetComponent<Rigidbody2D>();
        FacingDirection = 1;

        stateMachine.Inicialize(IdleState);
    }

    private void Update()
    {
        CurrentVelocity = PlayerRigidbody2D.velocity;
        stateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }

    #endregion

    #region Set Functions

    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        PlayerRigidbody2D.velocity = workspace;
        CurrentVelocity = workspace;
    }

    #endregion

    #region Check Funtions

    public void CheckIfShouldFlip(int xInput)
    {
        if(xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }

    #endregion

    #region Other Functions
    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    #endregion
}
