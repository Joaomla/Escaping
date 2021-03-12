using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    [Header("AttackRange Stats")]
    [SerializeField] [Range(0,Mathf.PI)] float angle = 0;

    public List<Enemy> enemiesWithinRange;

    private void Update()
    {
        //Debug.Log(enemiesWithinRange.Count);
    }

    private void OnTriggerEnter2D( Collider2D collision )
    {
        // if the other object is an enemy
        if(collision.GetComponent<Enemy>())
        {
            enemiesWithinRange.Add(collision.GetComponent<Enemy>());
        }
    }

    private void OnTriggerExit2D( Collider2D collision )
    {
        // the other agent is an enemy
        if(collision.GetComponent<Enemy>())
        {
            enemiesWithinRange.Remove(collision.GetComponent<Enemy>());
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector2 upper = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        Vector2 lower = new Vector2(Mathf.Cos(-angle), Mathf.Sin(-angle));
        Gizmos.DrawLine(gameObject.transform.position, gameObject.transform.position + new Vector3(lower.x, lower.y, 0));
        Gizmos.DrawLine(gameObject.transform.position, gameObject.transform.position + new Vector3(upper.x, upper.y, 0));
    }
}
