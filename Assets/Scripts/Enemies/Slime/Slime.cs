using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Slime : Enemy
{

    public StateMachine StateMachine = new StateMachine();

    public List<Transform> path = new List<Transform>();

    [SerializeField] public Collider2D trigger;
    //bool FoundPlayer = false;

    public bool isSlimeInCooldown = false;
    public float myvelocitySign;

    SpriteRenderer mysprite;

    public Rigidbody2D rb;
    public Vector2 pointOfMySpawn;

    [Header("Slime Wander Stats")]
    [SerializeField] public float wanderSpeed = 0.5f;
    [SerializeField] public Collider2D periscope;


    [Header("Slime Target Stats")]
    [SerializeField] public float targetedSpeed = 0.5f;
    [SerializeField] public float minDstToAtk = 3f;
    

    [Header("Slime Attack Stats")]
    [SerializeField] public float verticalSpeed = 4f;
    [SerializeField] public float horizontalSpeed = 2f;
    [SerializeField] public float coolDownTime = 2f;

    //TEST
    public void ReceivePath(List<Transform> path)
    {
        this.path = path;
    }   

    void Start() {
        EnemyInit();
        pointOfMySpawn = transform.position;
        rb = GetComponent<Rigidbody2D>();
        mysprite = GetComponent<SpriteRenderer>();
        StateMachine.ChangeState(new WanderSlime(this, path));

    }

    void FixedUpdate()
    {
        if(isSlimeInCooldown){return;}  //slime has a cooldown after attack this is where I apply it
        StateMachine.Update();
        GetHorizontalSpeed();
    }

    //need to explain this
    public bool SearchForPlayer()
    {   
        bool FoundPlayer = false;
        float castDst = 2f;

        Vector2 endPos = transform.position + Vector3.right * castDst * Mathf.Sign(myvelocitySign); 

        RaycastHit2D hit = Physics2D.Linecast(transform.position, endPos, 1 << LayerMask.NameToLayer("RaycastHits"));

        if(hit.collider != null)
        {
            if(hit.collider.gameObject.CompareTag("Player"))
            {
                Debug.Log("player hitted");
                FoundPlayer = true;
            }
            else
            {
                FoundPlayer = false;
            }
        }
        return FoundPlayer;
    }

    public override void GetsHurt( int damage, Vector2 origin )
    {
        // subtracts from the current health
        currentHealth = Mathf.Max(currentHealth - damage, 0);
        StartCoroutine(Blink());

        if (currentHealth == 0) Die();

        // knockback direction
        Vector2 knockbackDirection = (new Vector2(transform.position.x, transform.position.y) - origin).normalized;

        rb.velocity = new Vector2(receivedKnockbackValue * Mathf.Sign(knockbackDirection.x), receivedKnockbackValue * Mathf.Sign(knockbackDirection.y));
    }


/*void OnTriggerEnter2D(Collider2D other) 
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
    }*/

    void GetHorizontalSpeed()
    {
        bool slimeHasHorizontalSpeed = Mathf.Abs(myvelocitySign) > Mathf.Epsilon;
        if(slimeHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myvelocitySign), 1f);
        }
    }
}
