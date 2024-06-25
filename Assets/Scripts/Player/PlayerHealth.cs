using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;


public class PlayerHealth : MonoBehaviour
{
    [Header("Health Bar")]
    private float health;
    public float maxHealth = 200f;
    public Image healthBar;

    // Regeneration settings
    public float regenDelay = 5f; // Time in seconds before regeneration starts
    public float regenRate = 5f; // Health points regenerated per second
    private float lastDamageTime; // Time when the player last took damage

    [Header("DamageOverlay")]
    public Image overlay;
    public float duration;
    public float fadeSpeed;

    private float durationTimer;

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

        /*if (overlay.color.a > 0)
        {
            durationTimer += Time.deltaTime;
            if (durationTimer > duration)
            {
                float tempAlpha = overlay.color.a;
                tempAlpha -= Time.deltaTime * fadeSpeed;

                overlay.color = new Color(overlay.color.r, overlay.color.b, overlay.color.b, tempAlpha);
            }
        }*/
        

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
            overlay.color = new Color(overlay.color.r, overlay.color.b, overlay.color.b, 1-hFraction);
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
            overlay.color = new Color(overlay.color.r, overlay.color.b, overlay.color.b, 1-hFraction);
        }
    }
}
