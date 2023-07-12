using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController cc;
    public float moveSpeed;
    public float jumpForce;
    public float gravity;
    private Vector3 moveDir;

    // GetAxis test directional arrow keys
    void Update()
    {
        // Nous enregistrons un nouveau vector3 pour les axes horizontaux et verticaux (x, y, z) ce qui permet d'avoir les différents axes par défaut (projetSettings/InputManager)
        // Pour l'axe y, nous ne touchons rien car le personnage ne peut pas voler
        moveDir = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, moveDir.y, Input.GetAxis("Vertical") * moveSpeed);
        cc.Move(moveDir * Time.deltaTime);
    }
}
