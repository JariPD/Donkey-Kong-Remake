using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    idle = 0,
    walking = 1,
    attacking = 3,
    dead = 4
}     


public class PlayerAnim : MonoBehaviour
{
    /*[SerializeField] private PlayerState playerState;
    
    private PlayerMovement playerMovement;


    void Start()
    {
        
    }

    void Update()
    {
        if (playerState == PlayerState.dead && Input.GetKeyDown(KeyCode.Q))
        {
            ResetState();
        }
    }

    public void TriggerDeathAnimation()//zet playerstate op dead als je geen hartjes meer heb
    {
        playerState = PlayerState.dead;
        //GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<BarrelSpawner>().enabled = false;
    }

    public void ResetState()//reset zodat je niet voor altijd dood bent
    {
        playerState = PlayerState.idle;
        //GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<PlayerHealth>().ChangeHealth(3);
        GetComponent<PlayerMovement>().enabled = true;
        GetComponent<BarrelSpawner>().enabled = false;
    }*/
}
