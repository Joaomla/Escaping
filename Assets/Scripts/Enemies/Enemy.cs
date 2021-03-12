using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Health Stats")]
    [SerializeField] int maxHealth = 20;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
    }

    public void GetsHurt(int damage)
    {
        currentHealth = Mathf.Max(currentHealth - damage, 0);
        StartCoroutine(Blink());

        ////if (currentHealth == 0) gameObject.SetActive(false);
    }

    public void GetsHealed(int healValue)
    {
        currentHealth = Mathf.Min(currentHealth + healValue, maxHealth);
    }

    IEnumerator Blink()
    {
        // time per each blink (set of red - white colors)
        float timePerBlink = 0.5f;
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(timePerBlink); // wait a limited time
        GetComponent<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(timePerBlink); // wait a limited time
    }

}
