using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeSpawner : MonoBehaviour
{
    [SerializeField] private float currentTime = 10;
    [SerializeField] private float respawnTime = 15;
    [SerializeField] private GameObject snake;

    private List<GameObject> snakes = new List<GameObject>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= respawnTime)
        {
            currentTime = 0;
            Vector3 spawnPosition = new Vector3(-7.22f, 4.93f, 0f);
            GameObject newSnake = Instantiate(snake, new Vector3(-10,-6.5f,0), Quaternion.Euler(0, 0, 0));
            snakes.Add(newSnake);
        }
    }
}
