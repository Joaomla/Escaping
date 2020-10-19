﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // speed of the player in the x axis
    [SerializeField] float xSpeed = 4f;

    // jump height
    [SerializeField] float jumpHeight = 4f;

    // movement the player is doing in the x and y axis
    private float xMovement;
    private float yMovement;

    // checks if player can jump
    private bool isFalling = false;

    private Rigidbody2D rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        xMovement = Input.GetAxisRaw("Horizontal");

        float playerXMove = xMovement * xSpeed * Time.deltaTime;

        transform.position = new Vector3(transform.position.x + playerXMove, transform.position.y, transform.position.z);


        // Jumping
        if ( Input.GetKey(KeyCode.W) && isFalling == false )
        {
            rb.velocity = new Vector2( 0, jumpHeight );
        }

        isFalling = true;
    }

    private void OnCollisionStay2D( Collision2D collision )
    {
        isFalling = false;
    }
}
