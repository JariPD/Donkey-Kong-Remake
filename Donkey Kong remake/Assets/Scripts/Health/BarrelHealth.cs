using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelHealth : MainHealth
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("collision with Player");

            ChangeHealth(-3f);

            Destroy(gameObject);
        }

    }
}
