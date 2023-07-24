using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    private bool canInstantiate = true;
    private bool isInvincible = false;
    public GameObject pickupEffect;
    public GameObject mobEffect;
    public GameObject deathEffect;
    public GameObject loot;
    public bool animState = true;
    public GameObject cam1;
    public GameObject cam2;
    public GameObject cam3;
    public AudioClip hitSound;
    public AudioClip coinSound;
    public AudioClip deathSound;
    private AudioSource audioSource;
    public SkinnedMeshRenderer rend;
    public PlayerController playerController;
    Collider otherVar;

    // OnTriggerEnter detect all objects with the property "isTrigger(true)" in collision with the gameObject


    private void Start()
    {
        // On instancie la variable audioSource pour la connecter à notre composant, et ainsi accéder à ses méthodes telles que playOneShot pour jouer nos sons.
        audioSource = GetComponent<AudioSource>();
        playerController = GetComponent<PlayerController>();
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "coin")
        {
            // Quand une pièce est rammassée, une instance de notre système de particule est appelée, à la position de la pièce touchée, et une rotation de base (Quaternion.identity)
            // On enregistre le tout dans une variable de type GameObject pour pouvoir effectuer la suppression au cours du temps
            audioSource.PlayOneShot(coinSound);
            GameObject go = Instantiate(pickupEffect, other.transform.position, Quaternion.identity);
            Destroy(go, 0.5f);

            // Pattern sigleton pour accéder à une classe extérieure
            PlayerInfos.pi.GetCoin();
            Destroy(other.gameObject);
        }

        if (other.gameObject.name == "Fin")
        {
            print("Score final = " + PlayerInfos.pi.GetScore());
            SceneManager.LoadScene(2);
        }


        // Gestion de la caméra
        if(other.gameObject.tag == "cam1")
        {
            cam1.SetActive(true);
            playerController.camActive = 1;
        }
        
        else if (other.gameObject.tag == "cam2")
        {
            cam2.SetActive(true);
            playerController.camActive = 2;
        }

        else if (other.gameObject.tag == "cam3")
        {
            cam3.SetActive(true);
            playerController.camActive = 3;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        otherVar = other;
        // Invoke permet d'appeler une fonction avec un délai
        Invoke("MyOnTriggerExit", 0.2f);
    }

    public void MyOnTriggerExit (Collider other)
    {
        if (otherVar.gameObject.tag == "cam1")
        {
            cam1.SetActive(false);
            playerController.camActive = 0;
        }
        else if (otherVar.gameObject.tag == "cam2")
        {
            cam2.SetActive(false);
            playerController.camActive = 0;
        }

        else if (otherVar.gameObject.tag == "cam3")
        {
            cam3.SetActive(false);
            playerController.camActive = 0;
        }
    }


    // OnCollisionEnter detect all objects with the property "isTrigger(false"), solid objects
    // OnControllerColliderHit is the same, but works better with CC
    private void OnControllerColliderHit(ControllerColliderHit collision)
    {
       
        if (collision.gameObject.tag == "hurt" && !isInvincible)
        {
            print("You are wounded");
            isInvincible = true;

            // Utilisation de la fonction dans la classe PlayerInfos
            PlayerInfos.pi.SetHealth(-1);

            iTween.PunchPosition(gameObject, Vector3.back * 2, .5f);
            /*iTween.PunchScale(gameObject, new Vector3(0.2f, 0.2f, 0.2f), .5f);*/
            StartCoroutine("ResetInvincible");

        }

        if (collision.gameObject.tag == "mob" && canInstantiate)
        {
            // disabled collision on parent object to avoid getting hit
            collision.gameObject.transform.parent.gameObject.GetComponent<Collider>().enabled = false;
            canInstantiate = false;
            audioSource.PlayOneShot(hitSound);

            iTween.PunchScale(collision.gameObject.transform.parent.gameObject, new Vector3(50, 50, 50), .6f);
            print("Damage To Mob : 5");
            GameObject go = Instantiate(mobEffect, collision.transform.position, Quaternion.identity);

            // Quaternion.Euler takes the x y z as value of rotation
            Instantiate(loot, collision.transform.position + Vector3.forward, Quaternion.identity * Quaternion.Euler(90,0,0));
            Destroy(go.gameObject, 0.5f);
            // Destroy the parent object and not only the back of the mob
            Destroy(collision.gameObject.transform.parent.gameObject, 0.6f);

            // StartCoroutine est la seule manière d'appeler une fonction de type IEnumerator
            StartCoroutine("ResetInstanciate");
        }

        if (collision.gameObject.tag == "fall" && animState)
        {
            rend.enabled = false;
            animState = false;
            GameObject go = Instantiate(deathEffect, gameObject.transform.position, Quaternion.identity);
            audioSource.PlayOneShot(deathSound);
            StartCoroutine("DeathAnim");
        }
    }

    // IEnumerator est nécessaire pour mettre un script en pause
    IEnumerator ResetInstanciate() 
    {
        // Patienter quelques secondes
        yield return new WaitForSeconds(0.8f);
        canInstantiate = true;
    }


    IEnumerator ResetInvincible () 
    {
        for (int i = 0; i<10; i++)
        {
            yield return new WaitForSeconds(.2f);
            rend.enabled = !rend.enabled;    
            rend.material.color = Color.red;    
        }

        yield return new WaitForSeconds(.2f);
        rend.enabled = true;
        rend.material.color = Color.black;
        isInvincible = false;
    }

    IEnumerator DeathAnim() 
    {
        yield return new WaitForSeconds(1);
        animState = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        yield return new WaitForSeconds(1);
        rend.enabled = true;
    }
}
