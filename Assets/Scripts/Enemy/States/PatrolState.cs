using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    public int waypointIndex;
    public float waitTimer = 3f;
    private float waitTime;

    public override void Enter()
    {
        
    }
    public override void Perform()
    {
        PatrolCycle();
    }
    public override void Exit()
    {

    }

    public void PatrolCycle()
    {
        if (enemy.Agent.remainingDistance < 0.2f)
        {
            waitTime += Time.deltaTime;
            if (waitTime >= waitTimer)
            {
                if (waypointIndex < enemy.path.waypoints.Count - 1)
                    waypointIndex += 1;
                else
                {
                    waypointIndex = 0;
                }

                enemy.Agent.SetDestination(enemy.path.waypoints[waypointIndex].position);
                waitTime = 0;
            }
            
            
        }
    }

}
