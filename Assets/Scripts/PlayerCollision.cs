using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    
    public int nbCoins = 0;
    public GameObject pickupEffect;
    public GameObject mobEffect;

    // OnTriggerEnter detect all objects with the property "isTrigger(true)" in collision with the gameObject

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "coin")
        {
            // Quand une pièce est rammassée, une instance de notre système de particule est appelée, à la position de la pièce touchée, et une rotation de base (Quaternion.identity)
            // On enregistre le tout dans une variable de type GameObject pour pouvoir effectuer la suppression au cours du temps
            GameObject go = Instantiate(pickupEffect, other.transform.position, Quaternion.identity);
            Destroy(go, 0.5f);
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
            GameObject go = Instantiate(mobEffect, collision.transform.position, Quaternion.identity);
            Destroy(go.gameObject, 0.6f);
            // Destroy the parent object and not only the back of the mob
            Destroy(collision.gameObject.transform.parent.gameObject);
        }
    }
}
