using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Player player;
    Companion companion;

    // player body
    Rigidbody2D rb;
    EdgeCollider2D feet;

    // visuals
    Animator an;

    // Movement
    [Header("Horizontal Movement")]
    // speed of the player in the x axis
    [SerializeField] float xSpeed = 2.75f;
    [HideInInspector] public float xMovement;
    bool isFloored;
    int playerFacing = 1;

    // Jump
    [Header("Jump")]
    [SerializeField] float jumpHeight = 4.5f; // jump height
    [SerializeField] float jumpingTime = 0.2f;  // time the player can jump
    float currentJumpingTime;
    bool doJump;
    bool jumpKeyPressed = false;   // is the jump key pressed

    // Knockback
    [HideInInspector] public bool knockbacked;              // is the player knockbacked?
    [HideInInspector] public Vector2 dangerOrigin;          // origin of the dangerous agent
    [HideInInspector] public float knockbackValue;          // intensity of the knockback
    [HideInInspector] public float currentKnockbackTime;    // time of the knockback

    private void Awake()
    {
        player = GetComponent<Player>();
        companion = GameObject.Find("Companion").GetComponent<Companion>();

        rb = GetComponent<Rigidbody2D>();
        feet = GetComponent<EdgeCollider2D>();
        an = GetComponent<Animator>();
    }

    private void Start()
    {
        // jumping time
        currentJumpingTime = jumpingTime;
    }

    public void Check()
    {
        // Checks if the player is in the ground
        IsGrounded();
        // checks if player can jump
        Jump();
        // checks if can move
        Move();
        // animation related stuff
        Animate();
    }

    private void FixedUpdate()
    {
        // if player is being knockbacked, it can't move
        if (knockbacked)
        {
            xMovement = 0;
        }

        if (currentKnockbackTime > 0 )
        {
            // player becomes invincible for a while
            player.isInvincible = true;
            player.currentInvincibleTime = player.invincibilityTime;
            
            // knockback based on the player position and the danger position
            Vector2 playerPos = transform.position;
            Vector2 knockbackDirection = playerPos - dangerOrigin;
            
            player.rb.velocity = new Vector2(knockbackValue * Mathf.Sign(knockbackDirection.x), knockbackValue * Mathf.Sign(knockbackDirection.y));
            
            // time in which the knockback force is applied to the player
            currentKnockbackTime -= Time.fixedDeltaTime;
        }
        else if (doJump)
        {
            // Player can jump
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            isFloored = false;
            doJump = false;
            currentJumpingTime = jumpingTime;
        }
        else if ( jumpKeyPressed && rb.velocity.y > 0f && currentJumpingTime > 0)  // long jump
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
                if (currentJumpingTime > 0)
                {
                    currentJumpingTime -= Time.fixedDeltaTime;
                }
            }
        }
        else
        {
            if (knockbacked)
            {
                // if player is knockbacked, it will be thrown in the air, so the x velocity remains
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
                return;
            }

            // if player is in mid-air and there's no horizontal input, the player won't move in the x axis
            rb.velocity = new Vector2(0, rb.velocity.y);
            currentJumpingTime -= Time.fixedDeltaTime;
        }
    }

    // Checks if player is on the ground
    private void IsGrounded()
    {
        isFloored = false;

        // if the player feet is touching solidGround, they can jump
        if (feet.IsTouchingLayers(LayerMask.GetMask("SolidGround")))
        {
            knockbacked = false;    // reset knockback
            isFloored = true;       // is on the floor
            return;
        }
    }

    // Checks if player can jump
    private void Jump()
    {
        // Player can't just jump if they hold the jump key constantly. they have to release and press it again
        bool canJump = false;
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) canJump = true;

        // jumpkey is the variable for the long jump
        jumpKeyPressed = false;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) jumpKeyPressed = true;

        if ( canJump && isFloored )
        {
            doJump = true;
        }
    }

    // Checks if player moves
    private void Move()
    {
        xMovement = Input.GetAxisRaw("Horizontal");

        // update where the player is facing
        if (xMovement != 0 && (int)xMovement != playerFacing)
        {
            playerFacing = (int)xMovement;

            // the companion is currently a child of the player
            if(companion.transform.IsChildOf(transform))
            {
                // remove child companion, change size and reset child
                companion.transform.parent = null;
                transform.localScale = new Vector3(playerFacing, transform.localScale.y, transform.localScale.z);
                companion.transform.parent = transform;
            }
            else
            {
                // change size
                transform.localScale = new Vector3(playerFacing, transform.localScale.y, transform.localScale.z);
            }
        }
    }

    // Update animation
    private void Animate()
    {
        an.SetFloat("xMovement", Mathf.Abs(xMovement));
        an.SetBool("isJumping", false);
        if (!isFloored) an.SetBool("isJumping", true);
    }


}
