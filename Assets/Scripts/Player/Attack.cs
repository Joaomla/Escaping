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

    // is the player currently attacking?
    [HideInInspector] public bool isAttacking = false;

    private void Awake()
    {
        player = GetComponent<Player>();
        attackRange = GetComponentInChildren<AttackRange>();
    }

    public void Check()
    {
        // player can only attack if they are stopped
        if (player.rb.velocity != Vector2.zero) return;

        // Change input
        if(Input.GetKeyDown(KeyCode.Space))
        {
            // cooldown is not finished. can't attack
            if(!canAttack)
            {
                return;
            }

            canAttack = false;
            player.an.SetBool("isAttacking", true);
            isAttacking = true;
            StartCoroutine(AttackCooldown());
        }
    }

    public void AttackEvent()
    {
        // copies the attackRange list in case an enemy is despawn in the middle of the attack
        List<Enemy> enemies = new List<Enemy>(attackRange.enemies);

        // for each enemy within range, attack
        foreach (var enemy in enemies)
        {
            if (enemy == null) continue;

            // the enemy gets hurt
            enemy.GetsHurt(player.AttackValue, transform.position);
        }

        // reset attack variables
        player.an.SetBool("isAttacking", false);
        isAttacking = false;
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(cooldown);
        canAttack = true;
    }

}
