using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    PlayerMovement movementScript;
    Rigidbody2D rb;
    int doubleJumpCounter;
    // Start is called before the first frame update
    void Start()
    {
        movementScript = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (movementScript.isFloored) {
            doubleJumpCounter = 0;
            return; 
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            DoDoubleJump();
        }
    }

    void DoDoubleJump()
    {
        if(doubleJumpCounter > 0) {return; }
        doubleJumpCounter +=1;
        rb.velocity = new Vector2 (0, 10f);
    }
}
