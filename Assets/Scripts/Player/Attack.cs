using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    Player player;
    AttackRange attackRange;

    [Header("Attack Properties")]
    [SerializeField] float cooldown = 2f;
    bool canAttack = true;

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
            // cooldown is not finished. can't attack
            if(!canAttack)
            {
                return;
            }

            // for each enemy within range, attack
            foreach (var enemy in attackRange.enemiesWithinRange)
            {
                // Change value
                enemy.GetsHurt(player.AttackValue);
            }

            canAttack = false;
            StartCoroutine(AttackCooldown());
        }
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(cooldown);
        canAttack = true;
    }

}
