using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    
    public int nbCoins = 0;
    public GameObject pickupEffect;
    public GameObject mobEffect;
    private bool canInstantiate = true;
    public GameObject cam1;
    public GameObject cam2;
    public GameObject cam3;

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

        // Gestion de la caméra
        if(other.gameObject.tag == "cam1")
        {
            cam1.SetActive(true);
        }
        
        else if (other.gameObject.tag == "cam2")
        {
            cam2.SetActive(true);
        }

        else if (other.gameObject.tag == "cam3")
        {
            cam3.SetActive(true);
        }
    }


    private void OnTriggerExit(Collider other)
    {
         if (other.gameObject.tag == "cam1")
        {
            cam1.SetActive(false);
        }
        else if (other.gameObject.tag == "cam2")
        {
            cam2.SetActive(false);
        }

         else if (other.gameObject.tag == "cam3")
        {
            cam3.SetActive(false);
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

        if (collision.gameObject.tag == "mob" && canInstantiate)
        {
            canInstantiate = false;
            
            print("Damage To Mob : 5");
            GameObject go = Instantiate(mobEffect, collision.transform.position, Quaternion.identity);
            Destroy(go.gameObject, 0.6f);
            // Destroy the parent object and not only the back of the mob
            Destroy(collision.gameObject.transform.parent.gameObject);

            // StartCoroutine est la seule manière d'appeler une fonction de type IEnumerator
            StartCoroutine("ResetInstanciate");
        }
    }

    // IEnumerator est nécessaire pour mettre un script en pause
    IEnumerator ResetInstanciate() 
    {
        // Patienter quelques secondes
        yield return new WaitForSeconds(0.8f);
        canInstantiate = true;
    }
}
