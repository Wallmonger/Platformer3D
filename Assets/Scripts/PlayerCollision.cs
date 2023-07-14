using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    
    public int nbCoins = 0;
    // OnTriggerEnter detect all objects with the property "isTrigger(true)" in collision with the gameObject

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "coin")
        {
            nbCoins++;
            Destroy(other.gameObject);
        }
    }

    // OnCollisionEnter detect all objects with the property "isTrigger(false"), solid objects
    // OnControllerColliderHit is the same, but works better with CC
    private void OnControllerColliderHit(ControllerColliderHit collision)
    {
        if (collision.gameObject.tag == "hurt")
        {
            print("You are wounded");
        }

        if (collision.gameObject.tag == "mob")
        {
            print("Damage To Mob : 5");

            // Destroy the parent object and not only the back of the mob
            Destroy(collision.gameObject.transform.parent.gameObject);
        }
    }
}
