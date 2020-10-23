using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Searching : IState
{

    Companion companion;

    public Searching(Companion companion)
    {
        this.companion = companion;
    }
    public void Enter()
    {
        Debug.Log("Im entering");
        if (companion.CheckTrig()) 
        {
            Debug.Log("I found a point of interest");
            
        } 
    }

    public void Execute()
    {
        Debug.Log("Executing");
        if (companion.CheckTrig()) 
        {
            Debug.Log("I found a point of interest");
            companion.stateMachine.ChangeState(new LockingOn(companion));
        }
    }
    public void Exit() 
    {
        //not sure of what to do here
        Debug.Log("Exiting State");
    }

}
