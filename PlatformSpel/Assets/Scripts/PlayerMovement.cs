using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float MoveSpeed = 10;
    [SerializeField] float jumpSpeed = 3;
    [SerializeField] ContactFilter2D groundFilter;
    Vector2 moveInput;
    Rigidbody2D rb;
    bool isGrounded = true;
    int jumpTimes = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
    void OnJump()
    {
        if (jumpTimes == 1)
        {
            if (isGrounded == true)
            {
                jumpTimes = 0;
                rb.velocity += new Vector2(0f, jumpSpeed);
            }
        }
        else
        {
            jumpTimes++;
            rb.velocity += new Vector2(0f, jumpSpeed);
        }
    }
    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x*MoveSpeed, rb.velocity.y);
        rb.velocity = playerVelocity;

        if(moveInput.x != 0)
        {
            transform.localScale = new Vector2(Mathf.Sign(moveInput.x), transform.localScale.y);
        }
    }
    // Update is called once per frame
    void Update()
    {
        Run();
    }

    private void FixedUpdate()
    {
        isGrounded = rb.IsTouching(groundFilter);
    }
}
