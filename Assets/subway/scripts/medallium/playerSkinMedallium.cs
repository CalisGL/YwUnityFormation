using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSkinMedallium : MonoBehaviour
{
    public string ID;
    // Start is called before the first frame update
    void Start()
    {
        ID = PlayerPrefs.GetString("IDactual", "y001000");
        ActivateChildByID(ID);
    }

    // Update is called once per frame
    void Update()
    {
        // Si vous avez besoin d'activer un enfant en fonction d'une mise à jour dynamique,
        // vous pouvez appeler la méthode ActivateChildByID(ID) ici
        ActivateChildByID(ID);
    }

    void ActivateChildByID(string childName)
    {
        // Parcourir tous les enfants de l'objet parent
        foreach (Transform child in transform)
        {
            // Vérifier si le nom de l'enfant correspond à l'ID
            if (child.name == childName)
            {
                // Activer l'enfant si le nom correspond
                child.gameObject.SetActive(true);
            }
            else
            {
                // Désactiver les autres enfants
                child.gameObject.SetActive(false);
            }
        }
    }

    public void saveYokai()
    {
        PlayerPrefs.SetString("IDactual", ID);
    }
}
