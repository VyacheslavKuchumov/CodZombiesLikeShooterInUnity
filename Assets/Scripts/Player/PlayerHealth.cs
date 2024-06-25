using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private float health;
    public float maxHealth = 200f;
    public Image healthBar;

    // Regeneration settings
    public float regenDelay = 5f; // Time in seconds before regeneration starts
    public float regenRate = 5f; // Health points regenerated per second
    private float lastDamageTime; // Time when the player last took damage

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        lastDamageTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();

        

        // Check if enough time has passed since last damage to start regenerating health
        if (Time.time - lastDamageTime >= regenDelay)
        {
            RegenerateHealth();
        }
    }

    public void UpdateHealthUI()
    {
        /*Debug.Log(health);*/
        float fill = healthBar.fillAmount;
        float hFraction = health / maxHealth;

        if (fill > hFraction)
        {
            healthBar.fillAmount = hFraction;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        lastDamageTime = Time.time; // Update the last damage time
    }

    private void RegenerateHealth()
    {
        if (health < maxHealth)
        {
            health += regenRate * Time.deltaTime;
            float hFraction = health / maxHealth;
            healthBar.fillAmount = hFraction;
        }
    }
}
