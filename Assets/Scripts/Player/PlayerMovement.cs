﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float baseSpeed = 1f;
    public float speedModifier = 1f;

    private float inputHorizontal;
    private float inputVertical;
    private Vector3 force;

    private Rigidbody2D rb;
    private PlayerState playerState;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerState = GetComponent<PlayerState>();
    }

    private void Update()
    {
        if (playerState.CanWalk)
        {
            inputHorizontal = Input.GetAxis("Horizontal");
            inputVertical = Input.GetAxis("Vertical");

            force.Set(inputHorizontal, -inputVertical, 0f);
            force = force.normalized;
            playerState.CurrentDirection = force;
            rb.velocity = (force * (baseSpeed * speedModifier));

            if(force.Equals(Vector3.zero))
            {
                playerState.IsWalking = false;
            }
            else
            {
                playerState.IsWalking = true;
            }
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }
}
