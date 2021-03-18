using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRegenObject : MonoBehaviour
{
    [SerializeField] int healthRegenValue = 1;
    [SerializeField] bool fullRegen = false;

    private void OnTriggerEnter2D( Collider2D collision )
    {
        // this thing contacts the player -> player gets healed
        if (collision.GetComponent<Player>() != null)
        {
            // full regen
            if (fullRegen)
            {
                collision.GetComponent<Player>().GetsHealed();
                gameObject.SetActive(false);
                return;
            }

            // partial regen
            collision.GetComponent<Player>().GetsHealed(healthRegenValue);
            gameObject.SetActive(false);
        }
    }
}
