using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float Speed;
    [SerializeField] private float jumpForce;
    [Header("Jumper")]
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private Transform jumpCheckPoint;

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
        if (Input.GetKey(KeyCode.D))
        {
            transform.position = new Vector3(transform.position.x + (Speed / 100), transform.position.y);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.position = new Vector3(transform.position.x - (Speed / 100), transform.position.y);
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