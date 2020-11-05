using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoingToPlayer : IState
{
    Vector3 companionOriginalPos;
    Companion companion;
    public GoingToPlayer(Companion companion)
    {
        this.companion = companion;
    }
    public void Enter()
    {
        //Debug.Log("Entering GoingToPlayer State");
    }   

    public void Execute()
    {
        companion.transform.localPosition = Vector3.MoveTowards(companion.transform.localPosition, companion.myOriginalPosition, 2.5f * Time.deltaTime);
        if(companion.CheckTrig())
        {
            companion.stateMachine.ChangeState(new LockingOn(companion));
        }
    }

    public void Exit()
    {
        //Debug.Log("Exiting GoingToPlayer state");
    }
}
