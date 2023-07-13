using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController cc;

    // Variables pour le déplacement
    public float moveSpeed;
    public float jumpForce;
    public float gravity;

    // Vecteur directionnel souahaité
    private Vector3 moveDir;

    // GetAxis test directional arrow keys
    void Update()
    {   // ###  Calcul de la direction ###
        // Nous enregistrons un nouveau vector3 pour les axes horizontaux et verticaux (x, y, z) ce qui permet d'avoir les différents axes par défaut (projetSettings/InputManager)
        // Pour l'axe y, nous ne touchons rien car le personnage ne peut pas voler
        
        moveDir = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, moveDir.y, Input.GetAxis("Vertical") * moveSpeed);

        // ### Vérification de la touche espace depuis l'InputManager ###
        // CharacterCoontroller.isGrounded vérifie que le personnage touche une surface solide
        if (Input.GetButtonDown("Jump") && cc.isGrounded)
        {
            moveDir.y = jumpForce;
        }

        // ### Application de la gravité ###
        moveDir.y -= gravity * Time.deltaTime;


        // ### Si mouvement != 0 alors on applique une rotation ###
        if (moveDir.x != 0 || moveDir.z != 0)
        {
            // Slerp permet d'effectuer une rotation fluide (currentRotation, targetRotation, transitionSpeed) 
            // Look rotation prend en paramètre un new Vector3 pour calculer la rotation souhaitée
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3 (moveDir.x, 0, moveDir.z)), 0.5f);
        }

        // ### Déplacement ###
        cc.Move(moveDir * Time.deltaTime);
    }
}
