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

    // Health bar
    [SerializeField] int maxHealth = 10;
    private int currentHealth;
    [SerializeField] PowerBar healthbar;

    // Damage variables
    private Collider2D body;
    private bool isDamaged = false;
    private bool knockbacked = false;
    private Vector2 dangerOrigin;
    private float knockbackValue;
    private float currentKnockbackTime;
    private int damagedValue;

    // Invincible variables
    private bool invincible = false;
    [SerializeField] float invincibilityTime = 2f;
    private float currentInvincibleTime;

    private Rigidbody2D rb;
    private Animator an;
    private SpriteRenderer spriteRenderer;
    private Color currentSpriteColor = Color.white;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        feet = GetComponent<EdgeCollider2D>();
        body = GetComponent<BoxCollider2D>();

        // power bar
        powerBar.setMaxPower(maxPower);
        currentPower = maxPower;

        // health bar
        currentHealth = maxHealth;

        // jumping time
        currentJumpingTime = jumpingTime;
    }

    void Update()
    {
        // Checks if player is invincible
        Invincible();
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
        healthbar.SetPower(currentHealth);
    }

    private void FixedUpdate()
    {
        // if player is being knockbacked, it can't move
        if (knockbacked)
        {
            xMovement = 0;
        }

        if (isDamaged || currentKnockbackTime > 0 )
        {
            // player is damaged
            isDamaged = false;

            // player becomes invincible for a while
            invincible = true;
            currentInvincibleTime = invincibilityTime;
            
            // knockback based on the player position and the danger position
            Vector2 playerPos = transform.position;
            Vector2 knockbackDirection = playerPos - dangerOrigin;
            
            rb.velocity = new Vector2(knockbackValue * Mathf.Sign(knockbackDirection.x), knockbackValue * Mathf.Sign(knockbackDirection.y));
            
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

        if (feet.IsTouchingLayers(LayerMask.GetMask("SolidGround")))
        {
            knockbacked = false;
            isFloored = true;
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
        spriteRenderer.color = currentSpriteColor;
    }

    private void OnTriggerEnter2D( Collider2D collision )
    {
        CheckDanger(collision);
        CheckCollectible(collision);
    }

    private void CheckDanger( Collider2D collision )
    {
        if (!body.IsTouchingLayers(LayerMask.GetMask("Danger")))
        {
            // player is not touching anything dangerous
            return;
        }

        if (invincible)
        {
            // player is invincible, so no worries
            return;
        }

        // other object damages the player
        if (collision.GetComponent<ContactDamage>() != null)
        {
            knockbacked = true;
            isDamaged = true;

            // danger agent information
            dangerOrigin = collision.gameObject.transform.position;
            knockbackValue = collision.GetComponent<ContactDamage>().GetKnockback();
            damagedValue = collision.GetComponent<ContactDamage>().GetDmage();
            currentKnockbackTime = collision.GetComponent<ContactDamage>().GetKnockbackTime();

            currentHealth = healthbar.GetPower() - damagedValue;
        }
    }

    private void Invincible()
    {
        // the player is invincible
        if (currentInvincibleTime > 0)
        {
            currentSpriteColor = Color.red;
            currentInvincibleTime -= Time.deltaTime;
            return;
        }

        currentSpriteColor = Color.white;
        invincible = false;
    }

    private void CheckCollectible( Collider2D collision )
    {
        if (!body.IsTouchingLayers(LayerMask.GetMask("Collectible")))
        {
            // player is not touching any collectible
            return;
        }

        // other object heals the player
        if (collision.GetComponent<HealthRegen>() != null)
        {
            currentHealth = collision.GetComponent<HealthRegen>().HealthRegenValue();
            return;
        }
    }

}
