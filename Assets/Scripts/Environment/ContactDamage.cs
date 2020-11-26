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
}
