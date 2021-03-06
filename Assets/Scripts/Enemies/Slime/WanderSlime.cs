﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderSlime : IState
{
    Slime slime;

    public List<Transform> path = new List<Transform>();

    int pathIndex = 0;
    bool canFlip = true;
    //bool outOfBounds;

    int thingsInPeriscope;
    
    public WanderSlime(Slime slime, List<Transform> path)
    {
        this.slime = slime;
        this.path = path;
    

    }

    public void Enter()
    {
        
    }

    public void Execute()                               //We take a list of waypoints and move the slime through them, that's how it wanders
    {
        if (slime.SearchForPlayer())                      //if slime finds player we change state TODO: Use raycasts to find player
        {
            Player player = GameObject.FindObjectOfType<Player>();
            slime.StateMachine.ChangeState(new TargetPlayer(slime, player));
        }

        slime.rb.velocity = new Vector2(slime.wanderSpeed, 0f);
        if(Vector2.Distance(slime.transform.position, slime.pointOfMySpawn) >= 1f){
            //Debug.Log(Vector2.Distance(slime.transform.position, slime.pointOfMySpawn));
            // if the slime is facing the wrong direction, flip it
            if(slime.myvelocitySign == Mathf.Sign(slime.transform.position.x - slime.pointOfMySpawn.x))
            {
                //outOfBounds = true;
                Flip();
                canFlip = false;
            }
        }
        else
        {
            canFlip = true;
            //outOfBounds = false;
        }

        // the slime is hitting a wall
        RaycastHit2D hit = Physics2D.Linecast(slime.myorigin, slime.endPos, 1 << LayerMask.NameToLayer("SolidGround"));
        if (hit.collider != null)
        {
            if (hit.distance < 0.4f)
            {
                Flip();
            }
        }
        // slime is hitting a hole
        if (!slime.periscope.IsTouchingLayers(LayerMask.GetMask("SolidGround")))
        {
            
            Flip();
        }
        // slime is hitting spikes
        else if(slime.periscope.IsTouchingLayers(LayerMask.GetMask("Danger")))
        {
            
            Flip();
        }
        
        slime.myvelocitySign = Mathf.Sign(slime.wanderSpeed);
        
       /* if (pathIndex < path.Count - 1)
        {
            var targetPos = path[pathIndex + 1].position;
            slime.transform.position = Vector3.MoveTowards(slime.transform.position, targetPos, slime.wanderSpeed * Time.deltaTime);
            slime.myvelocitySign = targetPos.x - slime.transform.position.x;

            if (Vector3.Distance(slime.transform.position, targetPos) < 0.5f)
            {
                pathIndex++;
                if (pathIndex >= path.Count - 1) {pathIndex = -1;}
            }
        }*/
    }

    public void Exit()
    {

    }
    
    void Flip()
    {
        if(!canFlip){return;}
        slime.wanderSpeed = -slime.wanderSpeed;
    }
}
