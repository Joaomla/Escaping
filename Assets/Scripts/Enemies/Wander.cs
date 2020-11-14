using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : IState
{
    Enemy enemy;
    
    public List<Transform> path = new List<Transform>();

    int pathIndex = 0;


    public Wander(Enemy enemy,List<Transform> path)
    {
        this.enemy = enemy;
        this.path = path;
    }

    //move through the path
    public void Enter()
    {
        //enemy.transform.position = path[0].position;
    }

    public void Execute()
    {
        if (pathIndex < path.Count - 1)
        {
            var targetPos = path[pathIndex + 1].position;
            float speed = 0.4f * Time.deltaTime;
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, targetPos, speed);

            if (Vector3.Distance(enemy.transform.position, targetPos) < Mathf.Epsilon)
            {
                pathIndex++;
                if (pathIndex >= path.Count - 1) {pathIndex = -1;}
            }
        }
        
    }

    public void Exit()
    {

    }
   
}
