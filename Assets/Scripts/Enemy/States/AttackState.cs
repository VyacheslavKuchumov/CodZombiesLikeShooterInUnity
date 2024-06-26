using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    private float moveTimer;
    private float losePlayerTimer;
    /*public float waitBeforeSearchTimer;*/
    private float shotTimer = 0;

    public override void Enter()
    {
        shotTimer = 0;
    }

    public override void Exit()
    {
        
    }

    public override void Perform()
    {
        
        /*enemy.transform.LookAt(enemy.Player.transform);*/
        if (enemy.CanSeePlayer())
        {
            enemy.Agent.SetDestination(enemy.Player.transform.position);
            losePlayerTimer = 0;
            moveTimer += Time.deltaTime;
            shotTimer += Time.deltaTime;

            if (enemy.CanMelee()) {
                if (shotTimer > enemy.fireRate)
                {
                    MeleeAttack();
                    shotTimer = 0;
                }
            }
            

            if (moveTimer > Random.Range(1, 2))
            {
                enemy.Agent.SetDestination(enemy.transform.position + Random.insideUnitSphere*5);
                moveTimer = 0;
            }
            enemy.LastKnownPos = enemy.Player.transform.position;
            
        }
        else
        {
            losePlayerTimer += Time.deltaTime;
            if (losePlayerTimer > 3)
            {
                stateMachine.ChangeState(new SearchState());
                losePlayerTimer = 0;
            }
        }
        
    }


    public void MeleeAttack()
    {
        enemy.Player.GetComponent<PlayerHealth>().TakeDamage(50);
    }

    /*public void Shoot()
    {
        Transform gunBarrel = enemy.gunBarrel;

        GameObject bullet = GameObject.Instantiate(Resources.Load("Prefabs/Bullet") as GameObject, gunBarrel.position, enemy.transform.rotation);

        Vector3 shootDirection = (enemy.Player.transform.position - gunBarrel.transform.position).normalized;

        bullet.GetComponent<Rigidbody>().velocity = Quaternion.AngleAxis(Random.Range(-3f, 3f), Vector3.up) * shootDirection * 80;

        Debug.Log("shooting...");
        shotTimer = 0;
    }*/

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
