using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeData : MonoBehaviour
{
    public string prenom;

    private void Awake()
    {
        // L'objet ne sera pas d�truit entre les sc�nes
        DontDestroyOnLoad(this.gameObject);
    }


    private void OnMouseUp()
    {
        // R�cup�ration depuis le cube
        print("Depuis le cube : " + prenom);

        // R�cup�ration depuis les PlayersPrefs (disque dur)
        print("Depuis les playerPrefs : " + PlayerPrefs.GetString("Text"));
    }
}
