using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBehaviour : EnemyBehaviour
{
    public float JumpVelocity = 10f;
    public float Knockback = 20f;

    public float MaxSpeed = 5f;
    public float Acceleration = 1f;

    private Animator animator;
    private Rigidbody2D rigidBody;
    float horizontalMove = 0f;
    bool jump = false;
    bool facingRight = true;

    GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!target)
        {
            if (!(target = GameObject.FindGameObjectWithTag("Player")))
            {
                Destroy();
            }
        }

        if (target.transform.position.x > transform.position.x)
        {
            horizontalMove = 1;
        }
        else
        {
            horizontalMove = -1;
        }

        if (Random.value > 0.995 && rigidBody.velocity.y == 0)
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        if (horizontalMove == 0)
        {
            animator.Play("zombie-idle");
        }
        else
        {
            animator.Play("zombie-walk");
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

    public override void Destroy()
    {
        Destroy(gameObject);
        GameObject.FindGameObjectWithTag("EnemyController").GetComponent<EnemyController>().CurrentEnemies.Remove(gameObject);
    }

    public override void ApplyEffect(GameObject player)
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        playerHealth.currentHealth -= 20;

        if (player.transform.position.x > gameObject.transform.position.x)
        {
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(Knockback, Knockback);
        }
        else
        {
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(-Knockback, Knockback);
        }
    }
}
