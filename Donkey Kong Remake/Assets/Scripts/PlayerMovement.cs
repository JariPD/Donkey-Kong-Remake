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
    [SerializeField] private bool OnLadder = false;

    private Rigidbody2D rigidBody;

    void Start()
    {
        rigidBody = transform.GetComponent<Rigidbody2D>();
    }

    public bool IsGrounded()
    {
        return Physics2D.Raycast(jumpCheckPoint.transform.position, Vector3.down, 0.2f, groundLayers);
    }

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.W))
        {

        }
        else if (Input.GetKey(KeyCode.S))
        {
            RaycastHit2D[] Raycasts = Physics2D.BoxCastAll(transform.position, new Vector3(1,1,1), 0, Vector2.zero, 0);

            foreach (RaycastHit2D collide in Raycasts) 
            {
                if (collide.transform.CompareTag("Ladder"))
                {
                    print("Found");
                    transform.position = new Vector3(collide.transform.position.x, transform.position.y - (speed / 100));
                }
                else
                {
                    print(collide.transform.tag);
                }
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position = new Vector3(transform.position.x + (speed / 100), transform.position.y);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.position = new Vector3(transform.position.x - (speed / 100), transform.position.y);
        }

        if (Input.GetKey(KeyCode.Space))
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
}