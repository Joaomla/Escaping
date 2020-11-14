using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public StateMachine stateMachine = new StateMachine();

    [SerializeField] public List<Transform> path = new List<Transform>();

    void Start()
    {
        stateMachine.ChangeState(new Wander( this ,path));
    }

    void Update()
    {
        stateMachine.Update();
    }
}
