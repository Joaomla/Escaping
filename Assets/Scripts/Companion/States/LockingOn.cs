using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockingOn : IState
{
    Vector3 companionOriginalPos;
    Vector3 targetPos;
    Companion companion;
    Transform companionParent;


    public LockingOn(Companion companion)
    {
        //Debug.Log("entrei aqui agora");
        this.companion = companion;
        targetPos = companion.CheckTargetPos();
        companionOriginalPos = companion.myOriginalPosition;
        //Debug.Log(companionOriginalPos);
        companionParent = companion.transform.parent;
    }

    public void Enter()
    {
        //Debug.Log("Entering LockOn State");
        //Debug.Log (companionOriginalPos);
        companion.transform.parent = null;
        //have to check trig cuz it might change
    }

    public void Execute()
    {
        //Debug.Log("Executing LockOn State");
        companion.transform.position = Vector3.MoveTowards(companion.transform.position,targetPos,2.5f * Time.deltaTime);
        //Debug.Log(companion.CheckTrig());
        if(!companion.CheckTrig())
        {
            companion.stateMachine.ChangeState(new GoingToPlayer(companion));
        }
        //have to check trig cuz it might change
    }

    public void Exit() 
    {
        //Get monster back to it's original pos
        Debug.Log("executei a saída");
        companion.transform.parent = companionParent;
        //companion.transform.localPosition = companionOriginalPos;
    }
    
}
