using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelHealth : MainHealth
{
    private Score score;

    private void Start()
    {
        score = FindObjectOfType<Score>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            if (collision.gameObject.GetComponent<PlayerMovement>().hammerTime <= 0)
            {
                print("collision with Player");

                ChangeHealth(-3);

                Destroy(gameObject);
            }
            else
            {
                print("Hot");
                score.PointAdder(100);
                Destroy(gameObject);
            }

    }
}
