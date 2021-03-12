using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincible : MonoBehaviour
{
    Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    // Check if the player is currently invincible
    public void Check()
    {
        // the player is invincible
        if (player.currentInvincibleTime > 0)
        {
            if (player.currentInvincibleTime == player.invincibilityTime) StartCoroutine(Blink());
            player.currentInvincibleTime -= Time.deltaTime;

            return;
        }

        player.isInvincible = false;
    }

    // Blinks the player sprite with a red color
    IEnumerator Blink()
    {
        // number of blinks within the invincibility time interval
        float numberOfBlinks = player.numberOfBlinks;

        // time per each blink (set of red - white colors)
        float timePerBlink = player.invincibilityTime / (2 * numberOfBlinks - 1);

        for (int n = 0; n < numberOfBlinks; n++)
        {
            player.currentSpriteColor = Color.red;
            yield return new WaitForSeconds(timePerBlink); // wait a limited time
            player.currentSpriteColor = Color.white;
            yield return new WaitForSeconds(timePerBlink); // wait a limited time
        }
    }
}
