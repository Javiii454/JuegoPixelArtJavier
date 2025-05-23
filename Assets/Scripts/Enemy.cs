using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    //Movimiento

    public int direction = 1; 
    public float enemySpeed = 2;
    private Rigidbody2D rigidbody;
    public BoxCollider2D boxCollider;
    //Animacion
    private Animator _animator;
    private AudioSource _audioSource;
    //Audio
    public AudioClip audioClip;
    public AudioClip hitSFX;
    public AudioClip enemyDeathSFX;
    public float maxHealth = 5;         //La vida es un mierdon historico, ayudame silksong tytyty.
    public float currentHealth;
    private Slider healthBar;
    private GameManager gameManager;
    public float enemyDamage = 5;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        healthBar = GetComponentInChildren<Slider>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
    }

    void FixedUpdate()
    {
        rigidbody.velocity = new Vector2(enemySpeed * direction, rigidbody.velocity.y);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    public void Death()
    {
        gameManager.Kills();
        boxCollider.enabled = false;
        _audioSource.PlayOneShot(enemyDeathSFX);
        direction = 0;
        rigidbody.gravityScale = 0;
        _animator.SetTrigger("IsDead");
        
        Destroy(gameObject, 0.5f);
    }
     public void TakeDamage(float damage)
    {
        _audioSource.PlayOneShot(hitSFX);
        currentHealth-= (int) damage;
        healthBar.value = currentHealth;
        

        if(currentHealth <=0)
        {
            Death();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)  
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //Destroy(collision.gameObject);
            PlayerControler playerScript = collision.gameObject.GetComponent<PlayerControler>(); //variable que accede al playercontrol(script para ejecturar la funcion death que esta alli)
            playerScript.TakeDamage(enemyDamage);
            

        }

          

        if(collision.gameObject.CompareTag("Limite") || collision.gameObject.layer == 6)
        {
            direction *= -1;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //Destroy(collision.gameObject);
            PlayerControler playerScript = collision.gameObject.GetComponent<PlayerControler>(); //variable que accede al playercontrol(script para ejecturar la funcion death que esta alli)
            if(canAttack)
            {
                StartCoroutine(AttackDelay());
                playerScript.TakeDamage(enemyDamage);
            }

        }
    }

    bool canAttack = true;
    float attackDelay = 1;

    IEnumerator AttackDelay()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackDelay);
        canAttack = true;
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Limite") || collider.gameObject.layer == 6)
        {
            direction *= -1;
        }
    }

    void OnBecameVisible()
    {
        direction = 1;
        gameManager.enemiesInScreen.Add(gameObject);
    }

    void OnBecameInvisible()
    {
        direction = 0;
        gameManager.enemiesInScreen.Remove(gameObject);
    }
}
