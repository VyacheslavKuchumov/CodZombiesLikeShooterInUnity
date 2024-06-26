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
    
    


    void Start()
    {
        health = maxHealth;
        
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

    }

  


}
