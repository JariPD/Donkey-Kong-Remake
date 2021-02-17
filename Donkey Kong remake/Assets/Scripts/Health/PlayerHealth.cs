using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MainHealth
{
    //PlayerAnim playerAnim;

    protected override void Start()
    {
        base.Start();

        //playerAnim = GetComponent<PlayerAnim>();
    }

    private void Update()
    {
        CheckHealth();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            print("collision");

            ChangeHealth(-1);
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
