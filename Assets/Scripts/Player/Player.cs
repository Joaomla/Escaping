using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // player body
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public BoxCollider2D body;
    [HideInInspector] public EdgeCollider2D feet;

    // player movement
    PlayerMovement playerMovement;
    [HideInInspector] public float xMovement;        // horizontal movement of the player
    [HideInInspector] public int playerFacing = 1;   // side the player is facing (right)
    [HideInInspector] public bool isFloored = true;  // is the player on the ground

    // player's companion
    [Header("Companion")]
    [SerializeField] public Companion companion;

    // Player's Health
    [Header("Health Variables")]
    [SerializeField] int maxHealth = 10;
    [SerializeField] PowerBar healthbar = null;
    private int currentHealth;

    // Player's Power
    [Header("Power Variables")]
    [SerializeField] int maxPower = 100;
    [SerializeField] PowerBar powerBar = null;
    private int currentPower;

    // Abilities' Variables
    [Header("Abilities' Cost")]
    public int TeleportationCost = 10;
    Abilities abilities;
    [HideInInspector] public bool isTeleporting;

    // Invincible variables
    [Header("Invincibility variables")]
    public float invincibilityTime = 2f;
    public int numberOfBlinks = 5;          // number of blinks whilst invincible
    Invincible invincible;
    [HideInInspector]public bool isInvincible = false;
    [HideInInspector]public float currentInvincibleTime;

    //Visuals
    [HideInInspector] public Animator an;
    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public Color currentSpriteColor = Color.white;


    private void Awake()
    {
        companion = GameObject.Find("Companion").GetComponent<Companion>();
        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        feet = GetComponent<EdgeCollider2D>();
        body = GetComponent<BoxCollider2D>();

        playerMovement = GetComponent<PlayerMovement>();
        abilities = GetComponent<Abilities>();
        invincible = GetComponent<Invincible>();
    }

    private void Start()
    {
        // power bar
        powerBar.setMaxPower(maxPower);
        currentPower = maxPower;

        // health bar
        currentHealth = maxHealth;
    }

    private void Update()
    {
        // if the player is teleporting, stop. the player can't interact with anything
        if (isTeleporting) return;

        // Checks Abilities of the player - Companion
        abilities.Check();
        // Checks if player is invincible
        invincible.Check();
        // Checks if the player can move
        playerMovement.Check();
        // Updates the animation
        Animate();

    }

    // Adds a given amount of power to the powerbar and updates it
    public void AddPower( int power )
    {
        // current power is within the boundaries estabilished
        currentPower = Mathf.Min(maxPower, currentPower + power);
        currentPower = Mathf.Max(0, currentPower + power);
        // update powerbar
        powerBar.SetPower(currentPower);
    }

    // Update animation
    private void Animate()
    {
        spriteRenderer.color = currentSpriteColor;      // updates the color of the player sprite
    }

    // Hurts the player
    public void GetsHurt( int damage )
    {
        // if the player is invincible, fuck that
        if (isInvincible) return;

        // player's health is positive or 0, only
        currentHealth = Mathf.Max(currentHealth - damage, 0);
        healthbar.SetPower(currentHealth);
    }

    // Change contactDamage to be a class of dangerous objects/enemies
    public void GetsHurt( int damage, ContactDamage danger )
    {
        // if the player is invincible, fuck that
        if (isInvincible) return;

        // player's health is positive or 0, only
        currentHealth = Mathf.Max(currentHealth - damage, 0);
        healthbar.SetPower(currentHealth);

        playerMovement.dangerOrigin = danger.transform.position;           // position of the dangerous agent
        playerMovement.knockbackValue = danger.GetKnockback();             // knockback value of the dangerous agent
        playerMovement.currentKnockbackTime = danger.GetKnockbackTime();   // duration of the knockback
    }

    // player gets therapy
    public void GetsHealed( int healingValue )
    {
        // health is limited
        currentHealth = Mathf.Min(currentHealth + healingValue, maxHealth);
        healthbar.SetPower(currentHealth);
    }

    // the poor thing gets a full heal
    public void GetsHealed()
    {
        currentHealth = maxHealth;
        healthbar.SetPower(currentHealth);
    }
}
