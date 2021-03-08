using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Slime : MonoBehaviour
{
    public StateMachine StateMachine = new StateMachine();

    [SerializeField] public List<Transform> path = new List<Transform>();

    [SerializeField] public Collider2D trigger;
    bool FoundPlayer = false;

    Rigidbody2D rb;
        
    void Start() {
        rb = this.GetComponent<Rigidbody2D>();
        StateMachine.ChangeState(new WanderSlime(this, path));

    }

    void Update()
    {
        StateMachine.Update();
        Debug.Log(StateMachine.currentState);
    }


    public bool SearchForPlayer()
    {
        return FoundPlayer;
    }


    


    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            FoundPlayer = true;
        }
    }

    void OnTriggerStay2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            FoundPlayer = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            FoundPlayer = false;
        }
    }
}
