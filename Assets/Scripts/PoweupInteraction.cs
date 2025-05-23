using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoweupInteraction : MonoBehaviour
{
    private AudioSource audioSource;
    private BoxCollider2D boxCollider;
    public AudioClip SFX;
    private SpriteRenderer spriteRenderer;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Awake()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            gameManager.AddCoins();
            audioSource.PlayOneShot(SFX);
            spriteRenderer.enabled = false;
            Destroy(gameObject, 0.3f);
        }
        
    }

    
}
