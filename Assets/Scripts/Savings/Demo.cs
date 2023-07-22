using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Demo : MonoBehaviour
{
    public InputField savedText;

    public void SaveData()
    {
        // PlayerPrefs.SetString prend deux paramètres, la clé et la valeur (localStorage)
        PlayerPrefs.SetString("Text", savedText.text);

        // Récupération du cube et ajout dans son script de la valeur prenom.
        GameObject.Find("CubeDataContainer").GetComponent<CubeData>().prenom = savedText.text;
    }

    public void LoadData ()
    {
        // On récupère la clé et donc la valeur pour la stocker dans notre variable
        savedText.text = PlayerPrefs.GetString("Text");
    }

    public void changeScene()
    {
        SceneManager.LoadScene(4);
    }
}
