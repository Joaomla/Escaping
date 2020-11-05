using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : IState
{
    Enemy enemy;
    GameObject myEnemy;
    SpawnPointConfig spawnPointConfig;
    List<Transform> trajectorie = new List<Transform>();
    int pointIndex = 0;


    public Wander(GameObject myEnemy)
    {
        Debug.Log(spawnPointConfig.GetMyEnemy());
        trajectorie = spawnPointConfig.GetWaypoints();
        this.myEnemy = myEnemy;
    }

    //movetowards points A and B Back a Forth
    public void Enter()
    {
        //trajectorie = new List<Transform>();
        //trajectorie = spawnPointConfig.GetWaypoints();
    }

    public void Execute()
    {
        //Debug.Log(trajectorie.Count);
        if(pointIndex < trajectorie.Count - 1)
        {
            var targetPosition = trajectorie[pointIndex].transform.position;
            var movementThisFrame = enemy.GetWanderSpeed();
            myEnemy.transform.position = Vector3.MoveTowards(myEnemy.transform.position, targetPosition, movementThisFrame * Time.deltaTime);
            if(myEnemy.transform.position == targetPosition)
            {
                pointIndex++;
            }
        }
        
    }

    public void Exit()
    {

    }
   
}
