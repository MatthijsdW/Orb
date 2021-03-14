using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public float lifetime;
    public Color startColor, endColor;
    public Vector2 acceleration;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidBody;
    private float timer;

    public void Init(Vector2 position, float lifetime, Color startColor, Color endColor, Vector2 startVelocity, Vector2 acceleration)
    {
        timer = 0;
        transform.position = position;
        this.lifetime = lifetime;
        this.startColor = startColor;
        this.endColor = endColor;
        this.acceleration = acceleration;

        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = startVelocity;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = startColor;
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.color = Color.Lerp(startColor, endColor, timer / lifetime);

        rigidBody.velocity += acceleration;

        timer += Time.deltaTime;

        if (timer > lifetime)
        {
            Destroy(gameObject);
        }
    }
}
