using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    [Header("AttackRange Stats")]
    [SerializeField] float distance = 0.95f;  // range of attack
    [SerializeField] [Range(0,Mathf.PI)] float angle = 0; // angle range of attack
    [SerializeField] float xDeviation = 0f;  // deviation of the center of the circle

    // the collider of the range
    CircleCollider2D this_collider = null;

    // deviation in relation to the center of the player
    Vector3 deviation;

    // enemies that are near the player
    private List<Enemy> enemiesWithinRange = new List<Enemy>();
    // enemies that the player can actually attack
    public List<Enemy> enemies = new List<Enemy>();

    private void Awake()
    {
        this_collider = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        deviation = new Vector3(xDeviation, 0, 0);
        this_collider.radius = distance;
    }

    private void Update()
    {
        float scale = Mathf.Sign(transform.parent.transform.localScale.x);

        // for each enemies within range (distance), check if the angle is good for the player to attack
        foreach (Enemy enemy in enemiesWithinRange)
        {
            if (enemy == null) continue;

            // check angle
            float normDirection = Mathf.Deg2Rad*Vector3.Angle(enemy.transform.position - (transform.position - scale * deviation), scale * Vector2.right);

            // out of angle range
            if (-angle > normDirection || angle < normDirection)
            {
                // remove enemy from the list, if it exists
                enemies.Remove(enemy);
            }
            else
            {
                // enemy not in the list -> add
                if (!enemies.Contains(enemy)) enemies.Add(enemy);
            }
        }
    }

    private void OnTriggerEnter2D( Collider2D collision )
    {
        Enemy enemy = collision.GetComponent<Enemy>();

        // if the other object is an enemy
        if (enemy)
        {
            enemiesWithinRange.Add(enemy);
        }
    }

    private void OnTriggerExit2D( Collider2D collision )
    {
        Enemy enemy = collision.GetComponent<Enemy>();

        // the other agent is an enemy
        if(enemy)
        {
            enemiesWithinRange.Remove(enemy);
            enemies.Remove(enemy);
        }
    }

    private void OnDrawGizmos()
    {
        deviation = new Vector3(xDeviation, 0, 0); // deviation from the center of the player
        if(this_collider != null) this_collider.radius = distance;  // the range in which the player can attack

        Gizmos.color = Color.yellow;

        // lines with the upper/lower limits of the attack range
        Vector2 upper = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle))*distance;
        Vector2 lower = new Vector2(Mathf.Cos(-angle), Mathf.Sin(-angle))*distance;

        // the side the player is facing
        float scale = Mathf.Sign(transform.parent.transform.localScale.x);

        // draw the lines
        Gizmos.DrawLine(transform.position - scale * deviation, transform.position + scale * new Vector3(lower.x, lower.y, 0));
        Gizmos.DrawLine(transform.position - scale * deviation, transform.position + scale * new Vector3(upper.x, upper.y, 0));
    }
}
