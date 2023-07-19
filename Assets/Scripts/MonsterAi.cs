using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAi : MonoBehaviour
{
    // Input type range
    [Range(0.5f, 50)]
    public float detectDistance = 3;
    int destinationIndex = 0;
    public Transform[] points;
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent != null )
        {
            agent.destination = points[destinationIndex].position;
        }
    }

    private void Update()
    {
        // Agent.remainingDistance returns the distance left between the position of the gameObject, and its destination (defined in Start function)
        float dist = agent.remainingDistance;
        if (dist <= 0.05f) 
        {
            destinationIndex++;
            if (destinationIndex > points.Length -1)
            {
                destinationIndex = 0;
            }

            agent.destination = points[destinationIndex].position;
        }  
    }

    // Draw debug object when an object is selected
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectDistance);  
    }
}
