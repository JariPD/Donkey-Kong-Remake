using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MainHealth
{
    private void CheckHealth()
    {
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Kill();
        }

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            print("collision");

            ChangeHealth(-1f);
        }
    }
    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}
