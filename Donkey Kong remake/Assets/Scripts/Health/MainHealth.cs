using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHealth : MonoBehaviour
{
    [SerializeField] protected float maxHealth = 3;
    [SerializeField] protected float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    protected void ChangeHealth(float amount)
    {
        currentHealth = maxHealth + amount;
        CheckHealth();
    }

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
    public void Kill()
    {

    }
}
