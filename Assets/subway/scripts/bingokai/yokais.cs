using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public class yokais : MonoBehaviour
{
    public List<List<List<string>>> Yokai;

    void Start()
    {
        TextAsset textAsset;
        // Initialisation de la liste
        Yokai = new List<List<List<string>>>();

        // Ajout de sous-listes à la liste principale
        Yokai.Add(new List<List<string>>()); // 0 : tous les Yo-kai
        Yokai.Add(new List<List<string>>()); // 1 : Yokai possédés
        Yokai.Add(new List<List<string>>()); // 2 : Yokai non possédés

        // Ajout d'éléments à la première sous-liste (tous les Yo-kai)
        Yokai[0].Add(new List<string>()); // 0 Rang E
        Yokai[0].Add(new List<string>()); // 1 rang D
        Yokai[0].Add(new List<string>()); // 2 rang C
        Yokai[0].Add(new List<string>()); // 3 rang B
        Yokai[0].Add(new List<string>()); // 4 rang A
        Yokai[0].Add(new List<string>()); // 5 rang S

        // Ajout d'éléments à la deuxième sous-liste (Yo-kai possédés)
        Yokai[1].Add(new List<string>()); // 0 Rang E
        Yokai[1].Add(new List<string>()); // 1 rang D
        Yokai[1].Add(new List<string>()); // 2 rang C
        Yokai[1].Add(new List<string>()); // 3 rang B
        Yokai[1].Add(new List<string>()); // 4 rang A
        Yokai[1].Add(new List<string>()); // 5 rang S

        // Ajout d'éléments à la troisième sous-liste (Yo-kai non possédés)
        Yokai[2].Add(new List<string>()); // 0 Rang E
        Yokai[2].Add(new List<string>()); // 1 rang D
        Yokai[2].Add(new List<string>()); // 2 rang C
        Yokai[2].Add(new List<string>()); // 3 rang B
        Yokai[2].Add(new List<string>()); // 4 rang A
        Yokai[2].Add(new List<string>()); // 5 rang S

        // Ajout d'éléments à la première sous-liste (tous les Yo-kai, rang E)
        textAsset = Resources.Load<TextAsset>("E");
        string[] lines = textAsset.text.Split(',');
        Yokai[0][0].AddRange(lines);
        textAsset = Resources.Load<TextAsset>("D");
        lines = textAsset.text.Split(',');
        Yokai[0][1].AddRange(lines);
        textAsset = Resources.Load<TextAsset>("C");
        lines = textAsset.text.Split(','); 
        Yokai[0][2].AddRange(lines);
        textAsset = Resources.Load<TextAsset>("B"); 
        lines = textAsset.text.Split(',');
        Yokai[0][3].AddRange(lines);
        textAsset = Resources.Load<TextAsset>("A"); 
        lines = textAsset.text.Split(',');
        Yokai[0][4].AddRange(lines);
        textAsset = Resources.Load<TextAsset>("S"); 
        lines = textAsset.text.Split(',');
        Yokai[0][5].AddRange(lines);

        LoadYokaiListFromPlayerPrefs(5, "yokaiPossS");
        LoadYokaiListFromPlayerPrefs(4, "yokaiPossA");
        LoadYokaiListFromPlayerPrefs(3, "yokaiPossB");
        LoadYokaiListFromPlayerPrefs(2, "yokaiPossC");
        LoadYokaiListFromPlayerPrefs(1, "yokaiPossD");
        LoadYokaiListFromPlayerPrefs(0, "yokaiPossE");

        if (!Yokai[1][2].Contains("y001000"))
        {
            Yokai[1][2].Add("y001000");
        }
        // Affichage des éléments de la première sous-liste (tous les Yo-kai, rang E)
        Yokai[2][0].AddRange(Yokai[0][0].Except(Yokai[1][0]));
        Yokai[2][1].AddRange(Yokai[0][1].Except(Yokai[1][1]));
        Yokai[2][2].AddRange(Yokai[0][2].Except(Yokai[1][2]));
        Yokai[2][3].AddRange(Yokai[0][3].Except(Yokai[1][3]));
        Yokai[2][4].AddRange(Yokai[0][4].Except(Yokai[1][4]));
        Yokai[2][5].AddRange(Yokai[0][5].Except(Yokai[1][5]));

        foreach (string yokaiId in Yokai[0][0])
        {
            //Debug.Log(yokaiId);
        }

        foreach (string yokaiId in Yokai[1][0])
        {
            //Debug.Log(yokaiId);
        }
        foreach (string yokaiId in Yokai[1][1])
        {
            //Debug.Log(yokaiId);
        }
        foreach (string yokaiId in Yokai[1][2])
        {
            //Debug.Log(yokaiId);
        }
    }

    void Update()
    {
        // Logique de mise à jour si nécessaire
    }

    public void LoadYokaiListFromPlayerPrefs(int ListID, string nameOfList)
    {
        string json = PlayerPrefs.GetString(nameOfList);

        Debug.Log("Loaded Yokai List from PlayerPrefs: " + nameOfList);

        // Assurez-vous que la chaîne chargée n'est pas vide
        if (!string.IsNullOrEmpty(json))
        {
            // Convertir la chaîne en une liste en séparant les éléments par des virgules
            List<string> loadedList = new List<string>(json.Split(','));

            // Mettre à jour la liste yokais.Yokai[1][ListID] avec la liste chargée
            Yokai[1][ListID] = loadedList;

            Debug.Log("Yokai List Loaded: " + nameOfList + string.Join(", ", loadedList.ToArray()));
        }
        else
        {
            Debug.Log("No Yokai List found in PlayerPrefs: " + nameOfList);
        }
    }
}
