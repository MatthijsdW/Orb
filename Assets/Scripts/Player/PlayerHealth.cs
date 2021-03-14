using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth = 100;

    public float currentMana;
    public float maxMana = 100;

    public Image healthBar;
    public Image manaBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        currentMana = 0;
        Image[] images = GetComponentsInChildren<Image>();
        healthBar = images.FirstOrDefault(x => x.name == "HealthBar");
        manaBar = images.FirstOrDefault(x => x.name == "ManaBar");
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = currentHealth / maxHealth;
        manaBar.fillAmount = currentMana / maxMana;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
