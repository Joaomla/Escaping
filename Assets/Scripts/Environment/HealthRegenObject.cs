using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRegenItem : MonoBehaviour
{
    [SerializeField] int healthRegenValue = 1;
    [SerializeField] bool fullRegen = false;

    private void OnTriggerEnter2D( Collider2D collision )
    {
        // this thing contacts the player -> player gets healed
        if (collision.tag == "Player")
        {
            // full regen
            if (fullRegen)
            {
                collision.GetComponent<Player>().GetsHealed();
                return;
            }

            // partial regen
            collision.GetComponent<Player>().GetsHealed(healthRegenValue);
            gameObject.SetActive(false);
        }
    }
}
