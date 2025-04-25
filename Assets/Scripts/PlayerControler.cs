using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private float inputHorizontal;
    private float jumpPower = 4.5f;
    private float playerSpeed = 2f;
    public GroundSensor groundSensor;
    private Animator animator;
    [SerializeField] private float _dashForce = 20f;
    [SerializeField] private float _dashDuration = 0.5f;
    [SerializeField] private float _dashCooldwon = 1;
    private bool _canDash = true;
    private bool _isDashing = false; 
    // Start is called before the first frame update
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        groundSensor = GetComponentInChildren<GroundSensor>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");

        if(inputHorizontal > 0 )
        {
            transform.rotation = Quaternion.Euler(0,0,0);
            animator.SetBool("IsRunning", true);
        }
        else if(inputHorizontal < 0 )
        {
            transform.rotation = Quaternion.Euler(0,180,0);
            animator.SetBool("IsRunning", true);

        }
        else
        {
            animator.SetBool("IsRunning", false);
        }
        if(Input.GetButtonDown("Jump"))
        {
             if (groundSensor.isGrounded || groundSensor.canDoubleJump )
            {
                 Jump();
            }
           
            
        }    
        animator.SetBool("IsJumping", !groundSensor.isGrounded);
        
         if(Input.GetKeyDown(KeyCode.LeftShift) && _canDash)
        {
            StartCoroutine(Dash());

        }
    }
    void FixedUpdate()
    {
        if(_isDashing)
        {
            return;
        }
        rigidbody.velocity = new Vector2(inputHorizontal * playerSpeed, rigidbody.velocity.y);

        rigidbody.velocity = new Vector2(playerSpeed * inputHorizontal, rigidbody.velocity.y);
    }

    void Jump()
    {
       
        if(!groundSensor.isGrounded)
        {
            groundSensor.canDoubleJump = false;
            rigidbody.velocity = new Vector2(rigidbody.velocity.x,0);
        }
         rigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }

    IEnumerator Dash()
    {
        float gravity = rigidbody.gravityScale; 
        rigidbody.gravityScale = 0;
        rigidbody.velocity = new Vector2(rigidbody.velocity.x,0);
        _isDashing = true;
        _canDash = false; 
        rigidbody.AddForce(transform.right * _dashForce, ForceMode2D.Impulse); 
        yield return new WaitForSeconds(_dashDuration);
        rigidbody.gravityScale = gravity;
        _isDashing = false;
        yield return new WaitForSeconds(_dashCooldwon);
        _canDash = true;

    }


}
