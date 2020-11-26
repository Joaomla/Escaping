using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRegen : MonoBehaviour
{
    [SerializeField] PowerBar healthbar;
    [SerializeField] int healthRegenValue = 1;
    [SerializeField] bool fullRegen = false;

    public int HealthRegenValue()
    {
        // full regen of the life
        if( fullRegen )
        {
            return (int)healthbar.slider.maxValue;
        }

        return healthbar.GetPower() + healthRegenValue;
    }
}
