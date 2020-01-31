using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    private Vector3 currentDirection;
    public Vector3 CurrentDirection
    {
        set
        {
            currentDirection = value;
        }
    }

    private Vector3 lastDirection;
    public Vector3 LastDirection
    {
        set
        {
            lastDirection = value;
        }
    }

    private bool canWalk = true;
    public bool CanWalk
    {
        get
        {
            return canWalk;
        }

        set
        {
            canWalk = value;
        }
    }

    private bool isWalking = false;
    public bool IsWalking
    {
        get
        {
            return isWalking;
        }

        set
        {
            isWalking = value;
        }
    }

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();

        lastDirection = new Vector3();
    }

    private void Update()
    {
        UpdateLastDirection();
        UpdateAnimator();
    }

    private void UpdateLastDirection()
    {
        if(isWalking)
        {
            lastDirection.Set(currentDirection.x, currentDirection.y, currentDirection.z);
        }
    }

    private void UpdateAnimator()
    {
        anim.SetBool("isWalking", isWalking);
        anim.SetFloat("horizontalDirection", lastDirection.x);
        anim.SetFloat("verticalDirection", lastDirection.y);
    }
}
