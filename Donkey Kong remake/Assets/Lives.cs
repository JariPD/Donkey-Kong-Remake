using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lives : MonoBehaviour
{
   [SerializeField] private GameObject[] healthObjects;

    public void ChangeSprite(int amountOfHearts)
    {
        TurnAllHeartsOff();

        if (amountOfHearts <= 0)
        {
            amountOfHearts = 0;
        }

        healthObjects[amountOfHearts].SetActive(true);
    }

    private void TurnAllHeartsOff()
    {
        for (int i = 0; i < healthObjects.Length; i++)
        {
            healthObjects[i].SetActive(false);
        }
    }        
}
