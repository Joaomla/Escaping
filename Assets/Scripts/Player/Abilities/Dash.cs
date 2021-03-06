﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    float dashSpeed = 30f;
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
        dashDuration = -1;
    }
    // Update is called once per frame
    void Update()
    {
        
        //Debug.Log (movementScript.xSpeed);
        if (Input.GetKeyDown(KeyCode.A))
        {
            if ( buttonCooler > 0 && buttonCount >= 1 && inputLeft == 1)
            {
                playerMovementScript.xMovement = 0;
                dashDuration = 0.25f;
                dashLeft = true;
                DashAbility();                        /*perform dash*/
            }
            else
            {
                inputLeft = 1;
                inputRight = 0;
                buttonCooler = 0.5f ; 
                buttonCount += 1 ;
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("Input is D");
            if ( buttonCooler > 0 && buttonCount >= 1 && inputRight == 1)
            {
                playerMovementScript.xMovement = 0;
                dashDuration = 0.25f;
                dashLeft = false;
                DashAbility();                        /*perform dash*/
            }
            else
            {
                inputLeft = 0;
                inputRight = 1;
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
        Debug.Log("Duração do dash: " + dashDuration + " velocidade: " + dashSpeed * Time.deltaTime);
        if (dashLeft)
        {
            rb.velocity = new Vector2 (-dashSpeed, 0f);
        }
        else
        {
            rb.velocity = new Vector2 (dashSpeed, 0f);
        }
        isDashing = true;
        dashDuration -= 1 * Time.deltaTime;

    }
    
}
