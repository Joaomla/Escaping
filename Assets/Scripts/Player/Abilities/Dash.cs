using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    float dashSpeed = 2500f;
    float buttonCooler = 0.5f;
    int buttonCount = 0;
    bool dashLeft;
    float dashDuration;
    public bool isDashing;
    PlayerMovement playerMovementScript;
    Rigidbody2D rb;
    int inputLeft = 0;
    int inputRight = 0;


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
            Debug.Log("Input is A");
            if ( buttonCooler > 0 && buttonCount >= 1 && inputLeft == 1)
            {
                playerMovementScript.xMovement = 0;
                dashDuration = 40f * Time.deltaTime;
                dashLeft = true;
                DashAbility();                        /*perform dash*/
            }
            else
            {
                Debug.Log(buttonCount);
                inputLeft = 1;
                inputRight = 0;
                buttonCooler = 0.5f ; 
                buttonCount += 1 ;
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {

            if ( buttonCooler > 0 && buttonCount >= 1 && inputRight == 1)
            {
                playerMovementScript.xMovement = 0;
                dashDuration = 40 * Time.deltaTime;
                dashLeft = false;
                DashAbility();                        /*perform dash*/
            }
            else
            {
                inputLeft = 0;
                inputRight = 1;
                buttonCooler = 0.05f ; 
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
        if (dashLeft)
        {
            rb.velocity = new Vector2 (-dashSpeed * Time.deltaTime, 0f);
        }
        else
        {
            rb.velocity = new Vector2 (dashSpeed * Time.deltaTime, 0f);
        }
        isDashing = true;
        dashDuration -= Time.deltaTime;

    }
}
