using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSensor : MonoBehaviour
{
    public bool isGrounded;
    public bool canDoubleJump = true;
    private PlayerControler playerControl;

    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.layer == 3)
        {
            isGrounded = true;
            canDoubleJump = true;
        }

         if(collider.gameObject.layer == 8)
        {
            playerControl = GetComponentInParent<PlayerControler>();
            playerControl.Death();
        }
         if(collider.gameObject.layer == 7)
        {
            playerControl = GetComponentInParent<PlayerControler>();
            playerControl.Death();
        }
    }
    

    // Update is called once per frame
    void OnTriggerStay2D(Collider2D collider)
    {
         if(collider.gameObject.layer == 3)
        {
            isGrounded = true;
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        isGrounded = false;
    }
}
