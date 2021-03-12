using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttack : IState
{

    Slime slime;
    Player player;

    bool playerIsLeft;

    Rigidbody2D myRigidBody;

    public SlimeAttack(Slime slime, Player player)
    {
        this.slime = slime;
        this.player = player;
        myRigidBody = slime.GetComponent<Rigidbody2D>();
    }

    public void Enter()
    {
        WhereIsPlayer();                                                              //figure out where player is
        if(!playerIsLeft)
        {
            Vector2 jumpVelocityToAdd = new Vector2(slime.horizontalSpeed, slime.verticalSpeed);   //jump right
            myRigidBody.velocity += jumpVelocityToAdd;
        }
        else
        {
            
            Vector2 jumpVelocityToAdd = new Vector2(-slime.horizontalSpeed, slime.verticalSpeed);   //jump left
            myRigidBody.velocity += jumpVelocityToAdd;
        }
        
        slime.StartCoroutine(CoolDownPhase());
        
    }

    public void Execute()
    {
        WhereIsPlayer();
        if (Vector3.Distance(slime.transform.position,player.transform.position) < slime.minDstToAtk) //still check if player is in attack range
        {
            Enter();                                                                                  //if it is we attack again by entering this state again (we need a cooldown so maybe try couroutine here)
        }
        else
        {
            slime.StateMachine.ChangeState(new WanderSlime(slime,slime.path));                        //if player is not in range we get out
        }
        
    }

    public void Exit()
    {

    }

    public void WhereIsPlayer()                              //check player position to know wich side should I jump
    {
        if (slime.transform.position.x < player.transform.position.x )
        {
            playerIsLeft = false;
            slime.myvelocitySign = 1;
        }
        else
        {
            playerIsLeft = true;
            slime.myvelocitySign = -1;
        }
    }

    IEnumerator CoolDownPhase()                              //couroutine for attack cooldown
    {
        slime.isSlimeInCooldown = true;
        yield return new WaitForSeconds(slime.coolDownTime);
        slime.isSlimeInCooldown = false;
    }
}
