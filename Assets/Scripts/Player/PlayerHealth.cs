using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private float currentHealth = 20;
    public float CurrentHealth
    {
        get
        {
            return currentHealth;
        }

        set
        {
            currentHealth = value;
            if(currentHealth >= maximumHealth)
            {
                currentHealth = maximumHealth;
            }
            else if(currentHealth < 0)
            {
                currentHealth = 0;
            }
        }
    }

    private float maximumHealth = 20;
    public float MaximumHealth
    {
        get
        {
            return maximumHealth;
        }

        set
        {
            maximumHealth = value;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            CurrentHealth++;
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            CurrentHealth--;
        }
    }
}
