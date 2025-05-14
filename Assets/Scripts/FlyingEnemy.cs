using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public float speed = 1.5f;
    public bool chase = false; 
    private GameObject player;
    public Transform startingPoint;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        if(player==null)
        return;
        if(chase == true)
        Chase();
        else
        ReturnStartingPoint();
        Flip();
    }
    void Chase()
    {
        transform.position=Vector2.MoveTowards(transform.position,player.transform.position,speed * Time.deltaTime);

    }
    void Flip()
    {
        if(transform.position.x>player.transform.position.x)
        transform.rotation= Quaternion.Euler(0,0,0);
        else
        transform.rotation=Quaternion.Euler(0,180,0);
    }
    void ReturnStartingPoint()
    {
         transform.position=Vector2.MoveTowards(transform.position,startingPoint.transform.position,speed * Time.deltaTime);
    }
}
