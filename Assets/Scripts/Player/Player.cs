using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // speed of the player in the x axis
    [SerializeField] float xSpeed = 1.5f;

    // jump height
    [SerializeField] float jumpHeight = 4f;

    // movement of the player
    private float xMovement;
    // side the player is facing
    private int playerFacing = 1;

    // Jumping variables
    private bool isFloored = true; // is the player on the ground
    private bool doJump = false; // is the action to jump
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
                // player is in mid-air
                rb.velocity = new Vector2(xMovement * xSpeed, rb.velocity.y);
            }
        }
        else
        {
            // if player is in mid-air and there's no horizontal input, the player won't move in the x axis
            rb.velocity = new Vector2(0, rb.velocity.y);
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
        Debug.Log("isFloored: " + isFloored);
    }

    // Checks if player can jump
    private void Jump()
    {
        if ( ( Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) ) && isFloored)
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
