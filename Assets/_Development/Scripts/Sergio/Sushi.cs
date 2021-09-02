using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;

[RequireComponent(typeof(BoxCollider2D))]


public class Sushi : MonoBehaviour, IDamageableObject

{
    [SerializeField] private bool damaged = false;
    [SerializeField] private GameObject sushiParticle;

    // Start is called before the first frame update
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (damaged)
        {
            TakeDamage(0);
        }
    }
    public void TakeDamage(float damageAmount)
    {
        Instantiate(sushiParticle, transform.position, Quaternion.identity);
        Destroy(gameObject, 0f);
    }
    
}
