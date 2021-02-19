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
    private List<GameObject> usedBarrels = new List<GameObject>();

    [Header("Racket")]
    [SerializeField] public float hammerTime = 0;

    [Header("Animations")]
    [SerializeField] private Animator animator;
    [SerializeField] private float ladderFlipTime = 0.25f;
    [SerializeField] private float currentLadderFlipTime = 0.25f;

    private Score score;

    private Rigidbody2D rigidBody;


    void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = transform.GetComponent<Rigidbody2D>();
        score = FindObjectOfType<Score>();
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
        LadderMovement();
        CheckBarrel();
        RacketTime();

        if (pointsTimer < pointsTimerMax)
            pointsTimer += Time.deltaTime * 1;

       /* if (transform.position.x < -8.4f)
        {
            transform.position = new Vector3(-8.4f, transform.position.y, transform.position.z);
        }*/

        if (!IsGrounded() && CheckLadder(true) == null)
            SetAnimation(6);
    }

    private void LadderMovement()
    {
        if (onLadder == false && !IsGrounded())
            if (CheckLadder(true) != null /*&& CheckLadder(false) != null*/)
                return;

        if (hammerTime > 0)
            return;

        if (Input.GetKey(KeyCode.W))
        {
            SetAnimation(5);
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
            SetAnimation(5);
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
            SetAnimation(1);
            transform.eulerAngles = new Vector3(0, 180, 0);
            transform.position = new Vector3(transform.position.x + Time.deltaTime * speed, transform.position.y);
        }
        else if (Input.GetKey(KeyCode.A) && onLadder == false)
        {
            SetAnimation(1);
            transform.eulerAngles = new Vector3(0, 0, 0);
            transform.position = new Vector3(transform.position.x - Time.deltaTime * speed, transform.position.y);
        }

        if (hammerTime > 0)
            return;

        if (Input.GetKey(KeyCode.Space) && onLadder == false)
        {
            if (IsGrounded())
            {
                rigidBody.velocity = Vector2.up * jumpForce;
            }
        }

        if (!Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            SetAnimation(0);
    }

    private void CheckBarrel()
    {
        RaycastHit2D[] Raycasts = Physics2D.BoxCastAll(transform.position + boxPosition, boxOffset, 0, Vector2.zero, 0);
        foreach (RaycastHit2D collider in Raycasts)
        {
            for (int i = 0; i < usedBarrels.Count; i++)
                if (usedBarrels[i] == collider.transform.gameObject)
                {
                    //print("Found");
                    return;
                }

            if (collider.transform.CompareTag("Enemy") && pointsTimer >= pointsTimerMax)
            {
                usedBarrels.Add(collider.transform.gameObject);
                pointsTimer = 0f;
                score.PointAdder(100);
            }
        }
    }

    private void RacketTime()
    {
        if (hammerTime > 0)
        {
            hammerTime -= Time.deltaTime;

        }
        else if (hammerTime < 0)
        {
            hammerTime = 0;
        }
    }

    private void SetAnimation(int Value)
    {
        if (hammerTime > 0)
        {
            animator.SetInteger("StateValue", 4);
            return;
        }
        else if (Value == 5 && CheckLadder(true) != null)
        {

            animator.SetInteger("StateValue", 5);
            currentLadderFlipTime -= Time.deltaTime;
            if (currentLadderFlipTime <= 0)
            {
                transform.Rotate(0, 180, 0);
                currentLadderFlipTime = ladderFlipTime;
            }
            return;
        }
        else if (onLadder == true)
        {
            if (CheckLadder(true) != null)
                animator.SetInteger("StateValue", 5);
            //print("ok");
            return;
        }

        if (Value != 5) 
            animator.SetInteger("StateValue", Value);
    }
}