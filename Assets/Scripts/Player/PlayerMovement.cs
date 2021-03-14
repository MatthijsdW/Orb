using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float JumpVelocity = 10f;

    public float MaxSpeed = 10f;
    public float Acceleration = 1f;

    private Animator animator;
    private Rigidbody2D rigidBody;
    float horizontalMove = 0f;
    bool jump = false;
    bool facingRight = true;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {

        horizontalMove = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && rigidBody.velocity.y == 0)
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        if (horizontalMove == 0)
        {
            animator.Play("human-idle");
        }
        else
        {
            animator.Play("human-walk");
        }
        if ((facingRight && horizontalMove < 0) || (!facingRight && horizontalMove > 0))
        {
            facingRight = !facingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }

        if ((horizontalMove < 0 && rigidBody.velocity.x > -MaxSpeed) || (horizontalMove > 0 && rigidBody.velocity.x < MaxSpeed))
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x + horizontalMove * Acceleration, rigidBody.velocity.y);
        }

        if (jump)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, JumpVelocity);
        }
        jump = false;
    }
}
