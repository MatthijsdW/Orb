using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaBehaviour : MonoBehaviour
{
    public float acceleration;
    public int manaValue;

    State currentState = State.Idle;
    GameObject player;
    Rigidbody2D rigidBody;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (currentState == State.Idle)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x * 0.99f, rigidBody.velocity.y * 0.99f);

            if ((player.transform.position - transform.position).magnitude < 5f)
            {
                currentState = State.Moving;
            }
        }
        else if (currentState == State.Moving)
        {
            Vector3 target = player.transform.position;
            Vector3 direction = (target - transform.position).normalized;
            rigidBody.velocity += new Vector2(direction.x, direction.y) * acceleration;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<PlayerHealth>().currentMana += manaValue;
            Destroy(gameObject);
        }
    }

    enum State
    {
        Idle,
        Moving,
    }
}
