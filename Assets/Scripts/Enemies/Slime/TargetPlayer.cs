using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPlayer : IState
{
    Slime slime;
    Player player;

    Vector3 targetPos;
    bool attacked = false;

    Coroutine AttackCourotine;
    Rigidbody2D rb;
    

    public TargetPlayer(Slime slime, Player player)
    {
        this.slime = slime;
        this.player = player;
        rb = slime.GetComponent<Rigidbody2D>();
    }

    public void Enter() //We define our target position, since slimes can't fly we only target the player's X position
    {
        targetPos = new Vector3(player.transform.position.x, slime.transform.position.y, slime.transform.position.z);
        float myVelocity = targetPos.x - slime.transform.position.x;
        Debug.Log("target player");


    }

    public void Execute() //move towards target position
    {
        targetPos = new Vector3(player.transform.position.x, slime.transform.position.y, slime.transform.position.z);
        slime.transform.position = Vector3.MoveTowards(slime.transform.position, targetPos, slime.targetedSpeed * Time.deltaTime);
        slime.myvelocitySign = targetPos.x - slime.transform.position.x;

        if(!slime.SearchForPlayer())
        {
            slime.StateMachine.ChangeState(new WanderSlime(slime, slime.path));  //if player gets out of range we go back to wander
        }

        if(Vector3.Distance(player.transform.position, slime.transform.position) < slime.minDstToAtk) 
        {
            slime.StateMachine.ChangeState(new SlimeAttack(slime, player));       //if player get's too close we change to attack
        }
    }


    public void Exit()
    {
        
    }

  
}
