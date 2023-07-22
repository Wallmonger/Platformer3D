using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Demo : MonoBehaviour
{
    public InputField savedText;

    public void SaveData()
    {
        // PlayerPrefs.SetString prend deux param�tres, la cl� et la valeur (localStorage)
        PlayerPrefs.SetString("Text", savedText.text);
    }

    public void LoadData ()
    {
        // On r�cup�re la cl� et donc la valeur pour la stocker dans notre variable
        savedText.text = PlayerPrefs.GetString("Text");
    }
}
