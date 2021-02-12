using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TennisRacket : MonoBehaviour
{
    [SerializeField] private float totalTime = 0f;
    [SerializeField] private float Current;

    private void Update()
    {
        if (totalTime > 0)
        {
            totalTime -= Time.deltaTime;


        }
    }
}
