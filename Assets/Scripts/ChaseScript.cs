using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseScript : MonoBehaviour
{
    public FlyingEnemy[] enemyArray;
    // Start is called before the first frame update
    void OnTriggerEnter2D( Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            foreach (FlyingEnemy enemy in enemyArray)
            {
                enemy.chase = true;
            }
        }
    }

    // Update is called once per frame
    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            foreach (FlyingEnemy enemy in enemyArray)
            {
                enemy.chase = false; 
            }
        }
        
    }
}
