using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;

    public Transform healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar = transform.Find("HealthBar");
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.localScale = new Vector3(currentHealth / maxHealth, 1, 1);

        if (currentHealth <= 0)
        {
            GetComponent<EnemyBehaviour>().Destroy();
        }
    }
}
