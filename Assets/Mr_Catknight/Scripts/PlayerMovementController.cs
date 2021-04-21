using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{

    // Campos modificables
    [SerializeField] private float jumpForce = 200f; 
    [SerializeField] private float fallMultiplier = 1.5f; // El factor por el que se acelera el personaje al caer
    [Range(0f, 1f)] [SerializeField] private float movementSmoothing = 0.5f;
    [Range(0f, 1f)] [SerializeField] private float speedMultiplier = 0.5f;

    // Campos que podrian ser modificables :v
    private float maxGroundSlope = 60; // Inclinación máxima del suelo que pueda subir
    // Inclinaciones minima y maxima pared escalable
    private float minWallSlope = 80;
    private float maxWallSlope = 100;


    // Componentes jugador
    private Rigidbody2D rb;
    private Collider2D coll;

    // Informacion para checkear contactos y definir movimiento
    private ContactPoint2D groundContact, wallContact;
    private List<ContactPoint2D> currContacts = new List<ContactPoint2D>();
    private float moveDirection;
    private bool grounded;
    private int wasOnWall;

    Vector3 refVelocity = Vector3.zero;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        SetCollitions();
        

        // Acelera al personaje al caer, asi se siente mejor el salto
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

    }
    private void SetCollitions()
    {
        
        rb.GetContacts(currContacts); // Rellena la lista con los contactos de esa update de fisicas

        grounded = false;
        wasOnWall--;


        float currAngle;
        GameObject curr;
        foreach (ContactPoint2D cont in currContacts)
        {
            curr = cont.collider.gameObject;
            currAngle = Vector2.Angle(cont.normal, Vector2.up);

            if (currAngle <= maxGroundSlope && curr.tag.Equals("MoveSupport"))
            {
                groundContact = cont;
                grounded = true;
            }
            else if (currAngle >= minWallSlope && currAngle <= maxWallSlope && curr.tag.Equals("MoveSupport"))
            {
                wallContact = cont;
                wasOnWall = 3;
            }
        }
    }

    public void Move(float move, bool jump)
    {
        moveDirection = move;

        Vector3 targetVelocity;
        Vector2 perp = Vector2.Perpendicular(groundContact.normal);
        Vector2 normal = groundContact.normal.normalized;

        float realSpeed = speedMultiplier * 10;
        float wallFriction = 0.5f;


        if (grounded)
        {
            // Si esta en el piso la nueva velocidad es paralela al piso
            targetVelocity = new Vector3(-move * realSpeed * perp.x, -move * realSpeed * perp.y);
        }
        else
        {
            if (wasOnWall > 0)
            {
                // Si esta / estuvo hace poco en una pared entonces baja mas lento
                targetVelocity = new Vector3(move * realSpeed, rb.velocity.y * wallFriction);
            }
            else
            {
                targetVelocity = new Vector3(move * realSpeed, rb.velocity.y);
            }
        }

        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref refVelocity, movementSmoothing);

        Debug.DrawRay(coll.bounds.center, rb.velocity, Color.magenta);
        Debug.DrawRay(groundContact.point, groundContact.normal, Color.red);
        Debug.DrawRay(wallContact.point, wallContact.normal, Color.blue);




        if (jump)
        {
            if (grounded)
            {
                rb.AddForce(new Vector2(jumpForce * normal.x, jumpForce * normal.y));
            }
            else if (wasOnWall > 0)
            {
                float multiplierWallJump = Mathf.Sin(Mathf.PI / 4);
                
                rb.AddForce(new Vector2(-move * jumpForce * (Mathf.Sin(Mathf.PI / 32)), jumpForce * (1 + multiplierWallJump)));
            }
        }

        Debug.Log(string.Format("Grounded: {0} | WasOnWall {1}", grounded, wasOnWall));
    }

}
