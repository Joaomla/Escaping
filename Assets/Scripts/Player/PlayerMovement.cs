using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // speed of the player in the x axis
    [SerializeField] float xSpeed = 4f;

    // jump height
    [SerializeField] float jumpHeight = 4f;

    // movement of the player
    private float xMovement;
    [SerializeField] float dist2side = 0.5f;


    // Jumping variables
    private bool isFloored = true; // is the player on the ground
    private bool doJump = false; // is the action to jump
    [SerializeField] float dist2ground = 0.5f; // distance to the ground

    private Rigidbody2D rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Checks if the player is in the ground
        IsGrounded();

        // checks if the player can jump
        if ( Input.GetKey(KeyCode.W) && isFloored)
        {
            doJump = true;
        }

        // gets input of the x movement
        xMovement = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {

        if (doJump)
        {
            // Player can jump
            rb.velocity = new Vector2(0, jumpHeight);
            isFloored = false;
            doJump = false;
        }
        else if (xMovement != 0)
        {
            // player is moving horizontally
            if (isFloored)
            {
                // player is on the floor
                rb.MovePosition(new Vector2(transform.position.x + xMovement * xSpeed * Time.fixedDeltaTime, transform.position.y));
            }
            else
            {
                // player is mid-air
                rb.velocity = new Vector2(xMovement * xSpeed, rb.velocity.y);
            }
        }
        else
        {
            // if player is in mid-air and there's no horizontal input, the player won't move in the x axis
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    private void IsGrounded()
    {
        // debuging
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.down * dist2ground), Color.white);


        // list with all the hit objects
        RaycastHit2D[] allhit;

        // Shoot a ray out of the player's ass
        allhit = Physics2D.RaycastAll(transform.position, transform.TransformDirection(Vector2.down), dist2ground);
        
        // check if the ray hits solid ground that the player can jump from
        foreach (var hit in allhit)
        {
            // now filter by tag or name
            if (hit.transform.CompareTag("SolidGround"))
            {
                isFloored = true;
                return;
            }
        }

        isFloored = false;
    }

}
