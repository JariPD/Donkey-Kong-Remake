using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{    
    private int scoreValue = 0;
    [SerializeField] Text scoreText;

    private void Start()
    {
        PointAdder(0);
    }

    public void PointAdder(int value)
    {
        scoreValue += value;
        scoreText.text = "CurrentScore:" + scoreValue;
    }
    
}
