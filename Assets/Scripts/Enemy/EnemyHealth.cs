using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public GameObject baseEnemy;
    [Header("Health Bar")]
    private float health;
    public float maxHealth = 100f;

    public Color damageColor = Color.red; // Color to change to when damaged
    public float colorChangeDuration = 0.5f; // Duration of the color change

    private Color originalColor; // Original color of the enemy
    private MeshRenderer enemyRenderer; // Renderer of the enemy
    private bool isDamaged = false;


    void Start()
    {
        health = maxHealth;
        // Get the Renderer component attached to the enemy
        enemyRenderer = gameObject.GetComponent<MeshRenderer>();

        // Save the original color of the enemy
        if (enemyRenderer != null)
        {
            originalColor = enemyRenderer.material.color;
        }
        else
        {
            Debug.LogError("No Renderer found on the enemy GameObject.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);

        if (health == 0)
        {
            Destroy(baseEnemy);
        }
    }



    public void TakeDamage(float damage)
    {
        
        health -= damage;
        Debug.Log(health);
        if (!isDamaged)
        {
            StartCoroutine(ChangeColorTemporarily());
        }

    }

    private IEnumerator ChangeColorTemporarily()
    {
        isDamaged = true;

        // Change the color to the damage color
        enemyRenderer.material.color = damageColor;

        // Wait for the specified duration
        yield return new WaitForSeconds(colorChangeDuration);

        // Revert to the original color
        enemyRenderer.material.color = originalColor;

        isDamaged = false;
    }



}
