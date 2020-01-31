using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordAttack : MonoBehaviour
{
    public float baseAttackDamage = 1f;
    public float attackModifier = 1f;

    private bool inputAttack;

    private PlayerState playerState;

    private void Start()
    {
        playerState = GetComponent<PlayerState>();
    }

    private void Update()
    {
        if (playerState.CanAttack)
        {
            inputAttack = Input.GetButtonDown("SwordAttack");

            if(inputAttack)
            {
                playerState.IsAttacking = true;
            }
        }
    }
}
