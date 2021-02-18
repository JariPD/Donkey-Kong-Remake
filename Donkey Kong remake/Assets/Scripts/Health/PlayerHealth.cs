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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && transform.GetComponent<PlayerMovement>().hammerTime <= 0)
        {
            print("collision");

            ChangeHealth(-1);
            transform.position = new Vector3(-7.27f, -3.62f, 0);
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
