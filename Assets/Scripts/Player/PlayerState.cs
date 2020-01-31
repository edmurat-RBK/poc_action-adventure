using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    #region Variables

    //Player direction inputs
    private Vector3 currentDirection;
    public Vector3 CurrentDirection
    {
        set
        {
            currentDirection = value;
        }
    }

    //Last direction input
    private Vector3 lastDirection;
    public Vector3 LastDirection
    {
        set
        {
            lastDirection = value;
        }
    }

    //If true > The player is able to walk
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

    //If true > The player is walking
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

    //If true > The player is able to do a melee attack
    private bool canMeleeAttack = true;
    public bool CanMeleeAttack
    {
        get
        {
            return canMeleeAttack;
        }

        set
        {
            canMeleeAttack = value;
        }
    }

    //If true > The player is doing a melee attack
    private bool isMeleeAttacking = false;
    public bool IsMeleeAttacking
    {
        get
        {
            return isMeleeAttacking;
        }

        set
        {
            isMeleeAttacking = value;

            //When the player is doing a melee attack, he can't move or make an attack over the first
            if(value)
            {
                canWalk = false;
                canMeleeAttack = false;
            }
            else
            {
                canWalk = true;
                canMeleeAttack = true;
            }
        }
    }

    //Player Animator component
    private Animator anim;

    #endregion

    #region Methods

    #region Unity event

    //Unity Start event
    private void Start()
    {
        //Get Animator component
        anim = GetComponent<Animator>();
        //Init Vector
        lastDirection = new Vector3();
    }

    //Unity Update event
    private void Update()
    {
        UpdateLastDirection();
        UpdateAnimator();
    }

    #endregion

    #region Other methods

    //Method that update "lastDirection"
    private void UpdateLastDirection()
    {
        //If the player is walking (if player send an directional input)
        if(isWalking)
        {
            //Update "lastDirection" with "currentDirection"
            lastDirection.Set(currentDirection.x, currentDirection.y, currentDirection.z);
        }
        //Else if the player don't walk
        //Then "lastDirection" keeps the last directional inputs
    }

    //Method that update animator parameters
    private void UpdateAnimator()
    {
        //Send to the animator... 
        
        //...the player direction
        anim.SetFloat("horizontalDirection", lastDirection.x);
        anim.SetFloat("verticalDirection", lastDirection.y);
        //...the player state
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isMeleeAttacking", isMeleeAttacking);
    }

    //Method that get message sent by animation key events
    public void GetAnimationEvent(string message)
    {
        switch(message)
        {
            //"MeleeAttackEnded" is sent at the last frame of a melee attack animation
            case "MeleeAttackEnded":
                //Reset "isMeleeAttacking" player state
                IsMeleeAttacking = false;
                break;

            //"message" is unknown
            default:
                //Print a waning log in console
                Debug.LogWarning("PlayerState.GetAnimationEvent() received \"" + message + "\". Any action is assigned to this message");
                break;
        }
    }

    #endregion

    #endregion
}
