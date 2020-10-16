using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // speed of the player in the x axis
    [SerializeField] float xSpeed = 4f;

    // movement the player is doing in the x and y axis
    private float xMovement;
    private float yMovement;


    void Start()
    {
    }


    void Update()
    {
        xMovement = Input.GetAxisRaw("Horizontal");

        float playerXMove = xMovement * xSpeed * Time.deltaTime;

        transform.position = new Vector3(transform.position.x + playerXMove, transform.position.y, transform.position.z);

    }
}
