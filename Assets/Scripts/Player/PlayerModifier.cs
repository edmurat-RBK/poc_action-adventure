using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModifier : MonoBehaviour
{
    private float speedModifier = 1f;
    public float SpeedModifier
    {
        get
        {
            return speedModifier;
        }

        set
        {
            speedModifier = value;
        }
    }

    private float attackDamageModifier = 1f;
    public float AttackDamageModifier
    {
        get
        {
            return attackDamageModifier;
        }

        set
        {
            attackDamageModifier = value;
        }
    }

    private float meleeAttackDamageModifier = 1f;
    public float MeleeAttackDamageModifier
    {
        get
        {
            return meleeAttackDamageModifier;
        }

        set
        {
            meleeAttackDamageModifier = value;
        }
    }
}
