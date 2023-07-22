using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeData : MonoBehaviour
{
    public string prenom;

    private void Awake()
    {
        // L'objet ne sera pas détruit entre les scènes
        DontDestroyOnLoad(this.gameObject);
    }


    private void OnMouseUp()
    {
        // Récupération depuis le cube
        print("Depuis le cube : " + prenom);

        // Récupération depuis les PlayersPrefs (disque dur)
        print("Depuis les playerPrefs : " + PlayerPrefs.GetString("Text"));
    }
}
