using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private float inputHorizontal;
    private float jumpPower = 4.5f;
    private float playerSpeed = 5f;
    public GroundSensor groundSensor;
    // Start is called before the first frame update
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        groundSensor = GetComponentInChildren<GroundSensor>();

    }

    // Update is called once per frame
    void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");

        if(inputHorizontal > 0 )
        {
            transform.rotation = Quaternion.Euler(0,0,0);
        }
        if(inputHorizontal < 0 )
        {
            transform.rotation = Quaternion.Euler(0,180,0);

        }
        if(Input.GetButtonDown("Jump") && groundSensor.isGrounded == true)
        {
            Jump();
        }    
    }
    void FixedUpdate()
    {
        rigidbody.velocity = new Vector2(playerSpeed * inputHorizontal, rigidbody.velocity.y);
    }

    void Jump()
    {
        rigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }

}
