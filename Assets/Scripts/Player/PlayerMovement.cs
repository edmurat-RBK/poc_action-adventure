using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float baseSpeed = 1f;
    public float speedModifier = 1f;

    private float inputHorizontal;
    private float inputVertical;

    private Rigidbody2D rb;
    private Animator anim;
    private PlayerState playerState;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerState = GetComponent<PlayerState>();
    }

    private void Update()
    {
        if (playerState.CanWalk)
        {
            inputHorizontal = Input.GetAxis("Horizontal");
            inputVertical = Input.GetAxis("Vertical");

            Vector3 force = new Vector3(inputHorizontal, -inputVertical, 0f).normalized;
            playerState.CurrentDirection = force;
            rb.velocity = (force * (baseSpeed * speedModifier));

            if(!force.Equals(Vector3.zero))
            {
                playerState.IsWalking = true;
            }
            else
            {
                playerState.IsWalking = false;
            }
        }
    }
}
