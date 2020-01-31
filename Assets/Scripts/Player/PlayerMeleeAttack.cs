using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour
{
    #region Variables

    //Initial damage of melee attack hit
    public float baseMeleeAttackDamage = 1f;

    //If true > The player has pressed the melee attack input
    private bool inputAttack;

    //PlayerState component
    private PlayerState playerState;
    //PlayerModifier component
    private PlayerModifier playerModifier;

    #endregion

    #region Methods

    #region Unity event

    //Unity Start event
    private void Start()
    {
        //Get PlayerState
        playerState = GetComponent<PlayerState>();
    }

    //Unity Update event
    private void Update()
    {
        if (playerState.CanMeleeAttack)
        {
            inputAttack = Input.GetButtonDown("MeleeAttack");

            if(inputAttack)
            {
                playerState.IsMeleeAttacking = true;
            }
        }
    }

    #endregion

    #endregion
}
