using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbBehaviour : MonoBehaviour
{
    public GameObject manaDrop;
    public GameObject particle;

    public float movementSpeed = 100;
    public float range = 0.5f;
    public float strongRadius = 5f;
    public int basicDamage = 4;
    public int strongDamage = 10;

    public int basicKnockback = 10;
    public int strongKnockback = 20;

    public int idleParticleCount = 10;
    public int strongParticleCount = 100;

    private float particleTimeOverflow = 0;

    private float timeMoving;
    private GameObject owner;
    private PlayerHealth ownerStats;
    private State currentState = State.Idle;
    private Rigidbody2D rigidBody;

    // Start is called before the first frame update
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        owner = GameObject.FindGameObjectWithTag("Player");
        ownerStats = owner.GetComponent<PlayerHealth>();
        timeMoving = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        int particlesPerSecond = currentState == State.StrongAttack ? strongParticleCount : idleParticleCount;
        int particleCount = (int)((Time.deltaTime + particleTimeOverflow) * particlesPerSecond);
        particleTimeOverflow = (Time.deltaTime + particleTimeOverflow) - ((float)particleCount / particlesPerSecond);
        for (int i = 0; i < particleCount; i++)
        {
            Particle newParticle = Instantiate(particle).GetComponent<Particle>();
            newParticle.Init(
                transform.position + new Vector3((Random.value - 0.5f), (Random.value - 0.5f), 0), 
                1, 
                Color.red, 
                Color.yellow, 
                new Vector2(2 * (Random.value - 0.5f), 2 * (Random.value - 0.5f)), 
                new Vector2(0, 0.03f));
        }

        if (currentState == State.Idle && Input.GetMouseButtonDown(0))
        {
            Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = (target - transform.position).normalized;
            rigidBody.velocity = direction * movementSpeed;
            currentState = State.BasicAttack;
        }
        if (currentState == State.Idle && Input.GetMouseButtonDown(1) && ownerStats.currentMana >= 20)
        {
            ownerStats.currentMana -= 20;
            Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = (target - transform.position).normalized;
            rigidBody.velocity = direction * movementSpeed;
            currentState = State.StrongAttack;
        }
        else if (currentState == State.BasicAttack || currentState == State.StrongAttack)
        {
            timeMoving += Time.deltaTime;
            if (timeMoving > range)
            {
                currentState = State.Returning;
                timeMoving = 0;
            }
        }
        else if (currentState == State.Returning)
        {
            Vector3 target = owner.transform.position;
            Vector3 direction = (target - transform.position).normalized;
            rigidBody.velocity = direction * movementSpeed;
            
            if ((target - transform.position).magnitude < 1)
            {
                currentState = State.Idle;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject.tag == "Enemy" && currentState == State.BasicAttack)
        {
            collision.transform.gameObject.GetComponent<EnemyHealth>().currentHealth -= basicDamage;

            manaDrop.transform.position = transform.position;
            Instantiate(manaDrop).GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-3, 3), 3);
            Instantiate(manaDrop).GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-3, 3), 3);
            Instantiate(manaDrop).GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-3, 3), 3);

            if (collision.transform.position.x > gameObject.transform.position.x)
            {
                collision.GetComponent<Rigidbody2D>().velocity = new Vector2(basicKnockback, basicKnockback);
            }
            else
            {
                collision.GetComponent<Rigidbody2D>().velocity = new Vector2(-basicKnockback, basicKnockback);
            }
        }
        else if (currentState == State.StrongAttack)
        {
            List<GameObject> currentEnemies = GameObject.FindGameObjectWithTag("EnemyController").GetComponent<EnemyController>().CurrentEnemies;

            int particleCount = 100;
            for (int i = 0; i < particleCount; i++)
            {
                Particle newParticle = Instantiate(particle).GetComponent<Particle>();
                newParticle.Init(
                    transform.position + new Vector3((Random.value - 0.5f), (Random.value - 0.5f), 0),
                    3,
                    Color.red,
                    Color.yellow,
                    new Vector2(5 * (Random.value - 0.5f), 5 * (Random.value - 0.5f)),
                    new Vector2(0, 0.03f));
            }

            foreach (GameObject enemy in currentEnemies)
            {
                if ((transform.position - enemy.transform.position).magnitude < strongRadius)
                {
                    enemy.GetComponent<EnemyHealth>().currentHealth -= strongDamage;
                    if (enemy.transform.position.x > gameObject.transform.position.x)
                    {
                        enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(strongKnockback, strongKnockback);
                    }
                    else
                    {
                        enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(-strongKnockback, strongKnockback);
                    }
                }
            }
        }

        currentState = State.Returning;
        timeMoving = 0;
    }

    private void FixedUpdate()
    {
        if (owner == null)
        {
            Destroy(gameObject);
        }
        if (currentState == State.Idle)
        {
            transform.position = owner.transform.position;
        }
    }

    enum State
    {
        Idle,
        BasicAttack,
        StrongAttack,
        Returning,
    }
}
