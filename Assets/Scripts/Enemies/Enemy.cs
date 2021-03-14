using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("Health Stats")]
    [SerializeField] protected int maxHealth = 20;
    protected int currentHealth;

    [Header("Damage Stats")]
    [SerializeField] protected float receivedKnockbackValue = 5f;

    SpriteRenderer spriteRenderer;
    Color currentColor = Color.white;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected void EnemyInit()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        spriteRenderer.color = currentColor;
    }

    // this enemy receives damage
    public void GetsHurt(int damage)
    {
        currentHealth = Mathf.Max(currentHealth - damage, 0);
        StartCoroutine(Blink());

        if (currentHealth == 0) Die();
    }

    // this enemy receives damage and knockback
    public abstract void GetsHurt( int damage, Vector2 origin );
    

    // this enemy gets healed
    public void GetsHealed(int healValue)
    {
        currentHealth = Mathf.Min(currentHealth + healValue, maxHealth);
    }

    // the enemy blinks when receiving damage
    protected IEnumerator Blink()
    {
        // time per each blink (set of red - white colors)
        float timePerBlink = 0.5f;
        currentColor = Color.red;
        yield return new WaitForSeconds(timePerBlink); // wait a limited time
        currentColor = Color.white;
        yield return new WaitForSeconds(timePerBlink); // wait a limited time
    }

    // this enemy f*cking explodes
    protected void Die()
    {
        gameObject.SetActive(false);
        GameObject.Destroy(this);
    }
}
