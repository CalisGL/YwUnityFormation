using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ChildrenNames : MonoBehaviour
{
    public GameObject gameObject;
    public List<string> Liste = new List<string>();

    void Update()
    {
        // Effacez la liste pour éviter d'ajouter les mêmes noms à chaque frame
        Liste.Clear();

        // Parcourez tous les enfants du gameObject
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            // Ajoutez le nom de chaque enfant à la liste
            Liste.Add(gameObject.transform.GetChild(i).name);
        }

        // Écrivez les noms des enfants dans un fichier texte
        WriteToTextFile("S.txt", Liste);
    }

    void WriteToTextFile(string fileName, List<string> content)
    {
        // Chemin du fichier dans le répertoire Resources
        string filePath = Path.Combine(Application.dataPath, "Resources", fileName);

        // Écrivez les noms des enfants dans le fichier texte
        File.WriteAllLines(filePath, content.ToArray());
    }
}
