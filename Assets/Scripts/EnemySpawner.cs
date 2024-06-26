using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private Transform spawner;
    public float spawnDelay;
    private float spawnTimer;
    // Start is called before the first frame update
    void Start()
    {
        spawner = GetComponent<Transform>();
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;

        
        if (spawnTimer > spawnDelay )
        {
            GameObject.Instantiate(Resources.Load("Prefabs/CubertZombie") as GameObject, spawner.position, transform.rotation);
            spawnTimer = 0;
        }
         
    }
}
