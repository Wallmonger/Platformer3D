using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAi : MonoBehaviour
{
    // Input type range
    [Range(0.5f, 50)]
    public float detectDistance = 3;


    // Draw debug object when an object is selected
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectDistance);  
    }
}
