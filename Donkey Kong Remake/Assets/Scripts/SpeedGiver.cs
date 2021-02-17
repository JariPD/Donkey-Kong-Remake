using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedGiver : MonoBehaviour
{
    [SerializeField] private Vector3 Direction;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.transform.tag);
        if (collision.transform.CompareTag("Enemy") && collision.transform.GetComponent<Rigidbody2D>() != null)
        {
            print("Boost");
            collision.transform.GetComponent<Rigidbody2D>().AddForce(Direction);
        }   
        else
        {
            print(collision.transform.CompareTag("Enemy"));
            print(collision.transform.GetComponent<Rigidbody2D>());
        }
    }
}
