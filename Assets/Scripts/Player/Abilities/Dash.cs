using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    float dashSpeed = 15f;
    float buttonCooler = 0.5f;
    int buttonCount = 0;
    bool dashLeft;
    float dashDuration;
    public bool isDashing;
    PlayerMovement playerMovementScript;
    Rigidbody2D rb;
    private void Start() 
    {
        playerMovementScript = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        
        //Debug.Log (movementScript.xSpeed);
        if (Input.GetKeyDown(KeyCode.A))
        {
            if ( buttonCooler > 0 && buttonCount == 1/*Number of Taps you want Minus One*/)
            {
                playerMovementScript.xMovement = 0;
                dashDuration = 0.35f;
                dashLeft = true;
                DashAbility();                        /*perform dash*/
            }
            else
            {
                buttonCooler = 0.5f ; 
                buttonCount += 1 ;
            }
        } 
        if (dashDuration >= 0)
        {
            DashAbility();
        }
        else
        {
            isDashing = false;
        }

        if ( buttonCooler > 0 )
        {
 
        buttonCooler -= 1 * Time.deltaTime ;
 
        }
        else
        {
        buttonCount = 0 ;
        }
    }

    private void DashAbility()
    {
        if (dashLeft = true)
        {
            rb.velocity = new Vector2 (-dashSpeed, 0f);
            isDashing = true;
            dashDuration -= Time.deltaTime;
        }

    }

    private bool CanMove(Vector2 dir, float dst)
    {
        return Physics2D.Raycast(transform.position, dir, dst).collider == null;
    }
}
