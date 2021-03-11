using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Slime : MonoBehaviour
{

    //TODO: define the distances of attack and various speeds here
    public StateMachine StateMachine = new StateMachine();

    [SerializeField] public List<Transform> path = new List<Transform>();

    [SerializeField] public Collider2D trigger;
    bool FoundPlayer = false;

    public bool isSlimeInCooldown = false;

    Rigidbody2D rb;

    [Header("Slime Wander Stats")]
    [SerializeField] public float wanderSpeed;


    [Header("Slime Target Stats")]
    [SerializeField] public float targetedSpeed = 0.5f;
    [SerializeField] public float minDstToAtk = 3f;
    

    [Header("Slime Attack Stats")]
    [SerializeField] public float verticalSpeed = 4f;
    [SerializeField] public float horizontalSpeed = 2f;
    [SerializeField] public float coolDownTime = 2f;

        
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        StateMachine.ChangeState(new WanderSlime(this, path));

    }

    void Update()
    {
        if(isSlimeInCooldown){return;}  //slime has a cooldown after attack this is where I apply it
        StateMachine.Update();
        //Debug.Log(isSlimeInCooldown);
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
