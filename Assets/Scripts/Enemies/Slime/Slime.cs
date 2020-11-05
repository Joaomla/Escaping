using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField] List<Transform> path;
    public StateMachine stateMachine = new StateMachine();

    void Start()
    {
        stateMachine.ChangeState(new Wander(this.gameObject));
    }


    void Update()
    {
        stateMachine.Update();
    }
}
