using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Movimiento

    public int direction = 1; 
    public float enemySpeed = 2;
    private Rigidbody2D rigidbody;
    public BoxCollider2D boxCollider;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        rigidbody.velocity = new Vector2(enemySpeed * direction, rigidbody.velocity.y);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)  
    {
        if(collision.gameObject.CompareTag("Limite") || collision.gameObject.layer == 6)
        {
            direction *= -1;
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Limite") || collider.gameObject.layer == 6)
        {
            direction *= -1;
        }
    }
}
