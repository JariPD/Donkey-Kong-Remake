using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private Text text;

    private Rigidbody2D rigidBody;

    Score score;

    void Start()
    {
        rigidBody = transform.GetComponent<Rigidbody2D>();
        score = text.GetComponent<Score>();
    }

    public bool IsGrounded()
    {
        return Physics2D.Raycast(jumpCheckPoint.transform.position, Vector3.down, 0.2f, groundLayers);
    }

    public Transform CheckLadder(bool type)
    {
        Vector3 castPosition = transform.position;
        Vector3 castSize = new Vector3(0.1f, 0.1f, 0.1f);
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

        CheckBarrel();

        if (pointsTimer < pointsTimerMax)
            pointsTimer += Time.deltaTime * 1;

        if (transform.position.x < -8.4f)
        {
            transform.position = new Vector3(-8.4f, transform.position.y, transform.position.z);
        }
    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (CheckLadder(true) != null)
            {
                Vector3 playerPos = transform.position;
                Transform ladder = CheckLadder(true);
                if (onLadder == false)
                    onLadder = true;

                rigidBody.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                transform.position = new Vector3(ladder.position.x, playerPos.y + 0.002f, playerPos.z);
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
                Vector3 playerPos = transform.position;
                Transform ladder = CheckLadder(false);
                if (onLadder == false)
                    onLadder = true;

                rigidBody.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                transform.position = new Vector3(ladder.position.x, playerPos.y - 0.002f, playerPos.z);
            }
            else
            {
                onLadder = false;
                rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }


        if (Input.GetKey(KeyCode.D) && onLadder == false)
        {
            transform.position = new Vector3(transform.position.x + (speed / 100), transform.position.y);
        }
        else if (Input.GetKey(KeyCode.A) && onLadder == false)
        {
            transform.position = new Vector3(transform.position.x - (speed / 100), transform.position.y);
        }

        if (Input.GetKey(KeyCode.Space) && onLadder == false)
        {
            if (IsGrounded())
            {
                print("Hot");
                rigidBody.velocity = Vector2.up * jumpForce;
            }
            else
            {
                print(IsGrounded());
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
                 //+= 100;
            }
        }
    }
}