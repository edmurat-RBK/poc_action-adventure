using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour
{
    public float baseMeleeAttackDamage = 1f;
    public float attackModifier = 1f;

    private bool inputAttack;

    private PlayerState playerState;
    private PlayerModifier playerModifier;

    private void Start()
    {
        playerState = GetComponent<PlayerState>();
    }

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
}
