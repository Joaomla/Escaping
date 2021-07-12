using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    Player player;
    private Vector2 tpDestination;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    public void Check()
    {
        // Check for teleportation Ability
        TeleportAbility();
    }
    
    // Teleportation ability
    private void TeleportAbility()
    {
        bool canTP = false;

        // if the player is in movement, they cannot teleport
        if (player.rb.velocity != Vector2.zero) return;

        // If the player wants to teleport
        // Change input. this is a test
        if (Input.GetKeyDown(KeyCode.T))
        {
            player.companion.CheckIfCanTeleport(out canTP, out tpDestination);
        }

        // if the player can teleport, do it
        if (canTP)
        {
            player.isTeleporting = true;
            player.an.SetBool("isTeleporting", true);
            player.AddPower(-player.TeleportationCost); // Spends power
        }
    }

    public void TeleportEvent( int phase )
    {
        // the player just succumbed to darkness
        if (phase == 0)
        {
            player.rb.transform.position = tpDestination + new Vector2(0, 0.3f); // + a safe margin
        }
        else if (phase == 1)  // The player finished teleportation
        {
            player.isTeleporting = false;
            player.an.SetBool("isTeleporting", false);
        }
    }
}
