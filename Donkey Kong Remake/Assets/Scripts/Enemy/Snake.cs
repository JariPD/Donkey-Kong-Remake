using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField] private float currentTime = 0;
    [SerializeField] private float maxTime = 3;
    [SerializeField] private float walkTime = 0;
    [SerializeField] private bool direction = false;

    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= maxTime)
        {
            maxTime = Random.Range(3, 5);
            walkTime = 1;
            currentTime = 0;

            if (Random.Range(0, 2) == 0 && transform.position.x > -9.97f)
                direction = false;
            else
                direction = true;
        }

        if (walkTime > 0)
        {
            walkTime -= Time.deltaTime;
            if (direction == true)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                transform.position = transform.position + new Vector3(0.005f, 0, 0);
            }
            else if (direction == false)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                transform.position = transform.position - new Vector3(0.005f, 0, 0);
            }
        }
    }
}
