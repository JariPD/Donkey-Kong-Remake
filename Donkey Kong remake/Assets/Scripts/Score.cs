using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    
    public int ScoreValue = 0;

    [SerializeField] Text scoreText;

    void Start()
    {
        
    }

    void Update()
    {
        scoreText.text = "CurrentScore:" + ScoreValue;
    }
}
