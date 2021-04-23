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
    private float Horizontal;        // Controlar el movimiento horizontal
    private bool grounded;           // Para controlar si estamos en el suelo


    // Se inicia cuando llamamos al script por primera vez
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }


    // Se llama en cada frame del juego
    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        
    }

    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(Horizontal * Speed, Rigidbody2D.velocity.y);
    }
}
