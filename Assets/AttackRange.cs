using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    [Header("AttackRange Stats")]
    [SerializeField] float distance = 0.95f;
    [SerializeField] [Range(0,Mathf.PI)] float angle = 0;
    [SerializeField] float xDeviation = 0f;

    CircleCollider2D collider = null;

    Vector3 deviation;

    private List<Enemy> enemiesWithinRange = new List<Enemy>();
    public List<Enemy> enemies = new List<Enemy>();

    private void Awake()
    {
        collider = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        deviation = new Vector3(xDeviation, 0, 0);
    }

    private void Update()
    {
        float scale = Mathf.Sign(transform.parent.transform.localScale.x);

        // for each enemies within range (distance), check if the angle is good for the player to attack
        foreach (Enemy enemy in enemiesWithinRange)
        {
            // check angle
            float normDirection = Vector3.Angle(enemy.transform.position - (transform.position - scale * deviation), scale * Vector2.right);

            // out of angle range
            if(-angle > normDirection || angle < normDirection)
            {
                // remove enemy from the list, if it exists
                enemies.Remove(enemy);
            }
            else
            {
                // enemy is already in the list
                if (enemies.Contains(enemy)) continue;

                enemies.Add(enemy);
            }
        }

        Debug.LogError(enemies.Count);
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
        deviation = new Vector3(xDeviation, 0, 0);
        if(collider != null) collider.radius = distance;

        Gizmos.color = Color.yellow;
        Vector2 upper = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle))*distance;
        Vector2 lower = new Vector2(Mathf.Cos(-angle), Mathf.Sin(-angle))*distance;
        float scale = Mathf.Sign(transform.parent.transform.localScale.x);
        Gizmos.DrawLine(transform.position - scale * deviation, transform.position + scale * new Vector3(lower.x, lower.y, 0));
        Gizmos.DrawLine(transform.position - scale * deviation, transform.position + scale * new Vector3(upper.x, upper.y, 0));
    }
}
