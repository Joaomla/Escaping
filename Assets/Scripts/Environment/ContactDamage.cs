using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactDamage : MonoBehaviour
{
    [SerializeField] int damage = 1;
    [SerializeField] float knockback = 1;
    [SerializeField] float knockbackTime = 0.5f;

    public int GetDmage()
    {
        return damage;
    }

    public float GetKnockback()
    {
        return knockback;
    }

    public float GetKnockbackTime()
    {
        return knockbackTime;
    }

    private void OnTriggerEnter2D( Collider2D other )
    {
        //  this thing contacts the player -> player gets hurt
        if (other.GetComponent<Player>())
        {
            other.GetComponent<Player>().GetsHurt(damage, this);
        }
    }

    private void OnTriggerStay2D( Collider2D collision )
    {
        //  this thing contacts the player -> player gets hurt
        if (collision.GetComponent<Player>())
        {
            collision.GetComponent<Player>().GetsHurt(damage, this);
        }
    }
}
