using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Companion : MonoBehaviour
{
    // comportamento do monstro será programado através de state machine (try)
    
    public StateMachine stateMachine = new StateMachine();

    [SerializeField] PointChecker pointChecker;
    
    void Start()
    {
        stateMachine.ChangeState(new Searching(this));
        //pointChecker = FindObjectOfType<PointChecker>();
    }

    void Update()
    {
        stateMachine.Update();
    }


    //from this point onwards this is collision checks for points of interest. Need to change to accomodate several points
    public bool CheckTrig()
    {
        return pointChecker.trig;
    }

    public Vector3 CheckTargetPos()
    {
        return pointChecker.targetPos;
    }
    
}
