using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // speed of the player in the x axis
    [SerializeField] float xSpeed = 1.5f;
    // movement of the player
    private float xMovement;
    // side the player is facing
    private int playerFacing = 1;

    // Jumping variables
    [SerializeField] float jumpHeight = 4f; // jump height
    [SerializeField] float jumpingTime = 0.5f;  // time the player can jump
    private float currentJumpingTime;
    private bool isFloored = true; // is the player on the ground
    private bool doJump = false; // is the action to jump
    private bool jumpKey = false;   // is the jump key pressed
    private Collider2D feet;

    // Powerbar
    private int maxPower = 100;
    private int currentPower;
    [SerializeField] PowerBar powerBar;

    private Rigidbody2D rb;
    private Animator an;
    private SpriteRenderer spriteRenderer;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        feet = GetComponent<EdgeCollider2D>();
        powerBar.setMaxPower(maxPower);
        currentPower = maxPower;
        currentJumpingTime = jumpingTime;
    }

    void Update()
    {
        // Checks if the player is in the ground
        IsGrounded();
        // checks if player can jump
        Jump();
        // checks if can move
        Move();
        // Animation Update
        Animate();

        // test
        if (Input.GetKey(KeyCode.C)) currentPower--;
        if (Input.GetKey(KeyCode.V)) currentPower++;
        powerBar.SetPower(currentPower);
    }

    private void FixedUpdate()
    {
        Debug.Log("DoJump = " + doJump);
        if (doJump)
        {
            // Player can jump
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            isFloored = false;
            doJump = false;
            currentJumpingTime = jumpingTime;
        }
        else if ( jumpKey && rb.velocity.y > 0f && currentJumpingTime > 0)  // long jump
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            currentJumpingTime -= Time.fixedDeltaTime;
        }
        else if (xMovement != 0)
        {
            // player is moving horizontally
            if (isFloored)
            {
                // player is on the floor
                rb.velocity = new Vector2(xMovement*xSpeed, 0);
            }
            else
            {
                // player is in mid-air
                rb.velocity = new Vector2(xMovement * xSpeed, rb.velocity.y);
                currentJumpingTime -= Time.fixedDeltaTime;
            }
        }
        else
        {
            // if player is in mid-air and there's no horizontal input, the player won't move in the x axis
            rb.velocity = new Vector2(0, rb.velocity.y);
            currentJumpingTime -= Time.fixedDeltaTime;
        }
    }

    // Checks if player is on the ground
    private void IsGrounded()
    {
        if (feet.IsTouchingLayers(LayerMask.GetMask("SolidGround")))
        {
            isFloored = true;
            return;
        }
        isFloored = false;
    }

    // Checks if player can jump
    private void Jump()
    {
        // Player can't just jump if they hold the jump key constantly. they have to release and press it again
        bool canJump = false;
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) canJump = true;

        // jumpkey is the variable for the long jump
        jumpKey = false;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) jumpKey = true;

        if ( canJump && isFloored )
        {
            doJump = true;
        }
    }

    // Checks if player can move
    private void Move()
    {
        xMovement = Input.GetAxisRaw("Horizontal");

        // update where the player is facing
        if (xMovement != 0) playerFacing = (int)xMovement;
    }

    // Update animation
    private void Animate()
    {
        an.SetFloat("xMovement", Mathf.Abs(xMovement));
        an.SetBool("isJumping", false);
        if (!isFloored) an.SetBool("isJumping", true);
        spriteRenderer.flipX = (playerFacing != 1);
    }


}
