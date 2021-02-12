using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelSpawner : MonoBehaviour
{
    public GameObject enemyObject;
    public float secondsBetweenSpawn;
    public float elapsedTime = 0.0f;

    private List<GameObject> barrelList = new List<GameObject>();

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime > secondsBetweenSpawn)
        {
            elapsedTime = 0;
            Debug.Log(true);

            Vector3 spawnPosition = new Vector3(-7.22f, 4.93f, 0f);
            GameObject newEnemy = (GameObject)Instantiate(enemyObject, spawnPosition, Quaternion.Euler(0, 0, 0));

            barrelList.Add(newEnemy);
        }

        for (int i = 0; i < barrelList.Count; i++)
        {
            if (barrelList[i].transform.position.x < -10)
            {
                Destroy(barrelList[i]);
                barrelList.RemoveAt(i);
            }
        }
    }
}