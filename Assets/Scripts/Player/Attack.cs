using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    Player player;
    AttackRange attackRange;

    private void Awake()
    {
        player = GetComponent<Player>();
        attackRange = GetComponentInChildren<AttackRange>();
    }

    public void Check()
    {
        // Change input
        if(Input.GetKeyDown(KeyCode.Space))
        {
            foreach (var enemy in attackRange.enemiesWithinRange)
            {
                // Change value
                enemy.GetsHurt(player.AttackValue);
            }
        }
    }
}
