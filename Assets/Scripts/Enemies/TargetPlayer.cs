using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPlayer : IState
{
    Slime slime;
    Player player;

    float moveSpeed = .5f;
    float minDstToAtk = 5f;
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

    public void Enter()
    {
        targetPos = new Vector3(player.transform.position.x, slime.transform.position.y, slime.transform.position.z);


    }

    public void Execute()
    {
        targetPos = new Vector3(player.transform.position.x, slime.transform.position.y, slime.transform.position.z);
        slime.transform.position = Vector3.MoveTowards(slime.transform.position, targetPos, moveSpeed*Time.deltaTime);

        if(!slime.SearchForPlayer())
        {
            slime.StateMachine.ChangeState(new WanderSlime(slime, slime.path));
        }

    }


    public void Exit()
    {
        
    }

  
}
