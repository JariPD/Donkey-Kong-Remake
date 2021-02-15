using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHealth : MonoBehaviour
{
    [SerializeField] protected int maxHealth = 3;
    [SerializeField] protected int currentHealth;

    private Lives lives;

    protected virtual void Start()
    {
        currentHealth = maxHealth;

        lives = FindObjectOfType<Lives>();
    }

    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
        CheckHealth();
    }

    private void CheckHealth()
    {
        if (lives != null)
        {
            lives.ChangeSprite(currentHealth);
        }


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
        transform.gameObject.SetActive(false);
    }
}
