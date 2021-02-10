using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [Header("Jumper")]
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private Transform jumpCheckPoint;
    [Header("Ladder")]
    [SerializeField] private bool onLadder = false;
    [SerializeField] private LayerMask ladderLayers;
    [SerializeField] private Transform ladderCheckUp;
    [SerializeField] private Transform ladderCheckDown;
    [Header("Points")]
    [SerializeField] private float pointsTimer;
    [SerializeField] private float pointsTimerMax = 1;
    [SerializeField] private Vector3 boxPosition;
    [SerializeField] private Vector3 boxOffset;

    private Rigidbody2D rigidBody;

    void Start()
    {
        rigidBody = transform.GetComponent<Rigidbody2D>();
    }

    public bool IsGrounded()
    {
        return Physics2D.Raycast(jumpCheckPoint.transform.position, Vector3.down, 0.2f, groundLayers);
    }

    public Transform CheckLadder(bool type)
    {
        Vector3 castPosition = transform.position;
        Vector3 castSize = new Vector3(0.1f,0.1f,0.1f);
        if (type == true)
        {
            castPosition = ladderCheckUp.position;
            castSize = new Vector3(0.1f, 1f, 0.1f);
        }
        else if (type == false)
        {
            castPosition = ladderCheckDown.position;
            castSize = new Vector3(0.1f, 0.3f, 0.1f);
        }

        RaycastHit2D[] Raycasts2 = Physics2D.BoxCastAll(castPosition, castSize, 0, Vector2.zero, 0);

        if (Raycasts2 != null)
        {
            foreach (RaycastHit2D collide in Raycasts2)
            {
                if (collide.transform.CompareTag("Ladder"))
                {
                    return collide.transform;
                }
            }
        }
        return null;
    }

    void Update()
    {
        Movement();
        LadderMovement();

        CheckBarrel();

        if (pointsTimer < pointsTimerMax)
            pointsTimer += Time.deltaTime * 1;

        if (transform.position.x < -8.4f)
        {
            transform.position = new Vector3(-8.4f, transform.position.y, transform.position.z);
        }
    }

    private void LadderMovement()
    {
        if (onLadder == false && !IsGrounded())
            if (CheckLadder(true) != null && CheckLadder(false) != null)
                return;


        if (Input.GetKey(KeyCode.W))
        {
            if (CheckLadder(true) != null)
            {
                if (onLadder == false)
                    onLadder = true;

                Vector3 playerPos = transform.position;
                Transform ladder = CheckLadder(true);

                rigidBody.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                transform.position = new Vector3(ladder.position.x, playerPos.y + Time.deltaTime * 1f, playerPos.z);
            }
            else
            {
                onLadder = false;
                rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (CheckLadder(false) != null)
            {
                if (onLadder == false)
                    onLadder = true;

                Vector3 playerPos = transform.position;
                Transform ladder = CheckLadder(false);
                
                rigidBody.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                transform.position = new Vector3(ladder.position.x, playerPos.y - Time.deltaTime * 1f, playerPos.z);
            }
            else
            {
                onLadder = false;
                rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }
    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.D) && onLadder == false)
        {
            transform.position = new Vector3(transform.position.x + Time.deltaTime * speed, transform.position.y);
        }
        else if (Input.GetKey(KeyCode.A) && onLadder == false)
        {
            transform.position = new Vector3(transform.position.x - Time.deltaTime * speed, transform.position.y);
        }

        if (Input.GetKey(KeyCode.Space) && onLadder == false)
        {
            if (IsGrounded())
            {
                rigidBody.velocity = Vector2.up * jumpForce;
            }
        }
    }

    private void CheckBarrel()
    {
        RaycastHit2D[] Raycasts = Physics2D.BoxCastAll(transform.position + boxPosition, boxOffset, 0, Vector2.zero, 0);
        foreach (RaycastHit2D collider in Raycasts)
        {
            if (collider.transform.CompareTag("Enemy") && pointsTimer >= pointsTimerMax)
            {
                pointsTimer = 0f;
                //Points giver
            }
        }
    }
}