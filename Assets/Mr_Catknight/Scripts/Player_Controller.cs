using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Documentación:
 
 https://docs.unity3d.com/es/530/Manual/class-FrictionJoint2D.html
 https://docs.unity3d.com/ScriptReference/Collision2D.GetContacts.html

 */

public class Player_Controller : MonoBehaviour
{
    public float Speed;              // Para regular la velocidad
    public float JumpForce;          // Para regular la fuerza de salto

    private Rigidbody2D Rigidbody2D; // Para guardar referencia al Rigidbody del jugador
    private Animator Animator;
    private float horizontal;        // Controlar el movimiento horizontal
    private bool grounded;           // Para controlar si estamos en el suelo


    // Se inicia cuando llamamos al script por primera vez
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }


    // Se llama en cada frame del juego
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        if (horizontal < 0.0f) transform.localScale = new Vector3(-0.75f, 0.75f, 0.75f);
        else if(horizontal > 0.0f) transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);

        Animator.SetBool("walk", horizontal != 0.0f);

        Debug.DrawRay(transform.position, Vector3.down * 1f, Color.red);  // Test Raycast
        if (Physics2D.Raycast(transform.position, Vector3.down, 1f))
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
