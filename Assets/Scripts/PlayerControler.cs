using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    private GameManager _gameManager;
    private SoundManager _soundManager;
    private BoxCollider2D boxCollider;
    private AudioSource _audioSource;
    //disparo
    public Transform bulletSpawn;
    public GameObject bulletPrefab;
    public AudioClip shootSFX;
    public AudioClip deathSFX;
    public AudioClip jumpSFX;
    //Dispara si o no?
    public bool canShoot = false;
    //timerpowerup
    public float powerUpDuration = 10;
    public float powerUpTimer;
    public Image powerUpImage;



    // Start is called before the first frame update
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        groundSensor = GetComponentInChildren<GroundSensor>();
        animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        boxCollider = GetComponent<BoxCollider2D>();
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        _soundManager = FindObjectOfType<SoundManager>().GetComponent<SoundManager>();

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

        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
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

    public void Death()
    {
        animator.SetTrigger("IsDead");
        _audioSource.PlayOneShot(deathSFX);
        boxCollider.enabled = false;
        Destroy(groundSensor.gameObject);
        inputHorizontal = 0;
        rigidbody.velocity = Vector2.zero;
        rigidbody.AddForce(Vector2.up * jumpPower / 1.5f, ForceMode2D.Impulse);

        StartCoroutine(_soundManager.DeathBGM());//opcion 2: _soundManager.StartCoroutine("DeathBGM"); 

        //_soundManager.Invoke("DeathBGM", deathSFX.length); //el invoke te permite llamar a una funcion pero meterle un tiempo de cooldown sabes
        //_soundManager.DeathBGM();

            _gameManager.isPlaying = false;

        Destroy(gameObject, 3);

       


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
    void Shoot()
    {
        Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        _audioSource.PlayOneShot(shootSFX);
    }
    


}
