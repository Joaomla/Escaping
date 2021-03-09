using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderSlime : IState
{
    Slime slime;

    public List<Transform> path = new List<Transform>();

    int pathIndex = 0;
    
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
        if (pathIndex < path.Count - 1)
        {
            var targetPos = path[pathIndex + 1].position;
            float speed = 0.5f * Time.deltaTime;
            slime.transform.position = Vector3.MoveTowards(slime.transform.position, targetPos, speed);

            if (Vector3.Distance(slime.transform.position, targetPos) < 0.5f)
            {
                pathIndex++;
                if (pathIndex >= path.Count - 1) {pathIndex = -1;}
            }
        }

        if(slime.SearchForPlayer())                      //if slime finds player we change state TODO: Use raycasts to find player
        {
            Player player = GameObject.FindObjectOfType<Player>();
            slime.StateMachine.ChangeState(new TargetPlayer(slime, player));
        }
    }

    public void Exit()
    {

    }
}
