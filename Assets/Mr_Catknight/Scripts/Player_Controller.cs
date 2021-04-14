using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public float Speed;              // Para regular la velocidad
    public float JumpForce;          // Para regular la fuerza de salto

    private Rigidbody2D Rigidbody2D; // Para guardar referencia al Rigidbody del jugador
    private float horizontal;        // Controlar el movimiento horizontal
    private bool grounded;           // Para controlar si estamos en el suelo


    // Se inicia cuando llamamos al script por primera vez
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Se llama en cada frame del juego
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        Debug.DrawRay(transform.position, Vector3.down * 0.1f, Color.red);  // Test Raycast
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.1f))
        {
            grounded = true;
        }
        else grounded = false;

        if (Input.GetKeyDown(KeyCode.W) && grounded) 
        {
            Jump();
        }

    }

    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(horizontal * Speed, Rigidbody2D.velocity.y);
    }
}
