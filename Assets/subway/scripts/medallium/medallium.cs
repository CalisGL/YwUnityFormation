using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class medallium : MonoBehaviour
{
    public List<string> yokai;
    public List<RectTransform> pages; // Reference to the RectTransform of the page
    public float turnSpeed = 1.0f; // Speed at which the page turns
    public RectTransform page;
    public int ActualPage = 1;
    public bool canTurn = true;
    public GameObject couverture;

    // Start is called before the first frame update
    void Start()
    {
        LoadYokaiListFromPlayerPrefs(5, "yokaiPossS");
        LoadYokaiListFromPlayerPrefs(4, "yokaiPossA");
        LoadYokaiListFromPlayerPrefs(3, "yokaiPossB");
        LoadYokaiListFromPlayerPrefs(2, "yokaiPossC");
        LoadYokaiListFromPlayerPrefs(1, "yokaiPossD");
        LoadYokaiListFromPlayerPrefs(0, "yokaiPossE");

        if (!yokai.Contains("y001000"))
        {
            yokai.Add("y001000");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ActualPage < 2 && pages[0].localRotation.eulerAngles.y < 90f)
        {
            //pages[0].gameObject.SetActive(true);
            couverture.SetActive(false);
        }
        else if (pages[0].localRotation.eulerAngles.y > 90f)
        {
            //pages[0].gameObject.SetActive(false);
            couverture.SetActive(true);
        }


        if(ActualPage > 2)
        {
            //pages[ActualPage - 1].gameObject.SetActive(false);
            //pages[ActualPage + 1].gameObject.SetActive(true);
        }
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

            // Ajouter les éléments de la liste chargée à la liste yokai
            yokai.AddRange(loadedList);

            Debug.Log("Yokai List Loaded: " + nameOfList + string.Join(", ", loadedList.ToArray()));
        }
        else
        {
            Debug.Log("No Yokai List found in PlayerPrefs: " + nameOfList);
        }
    }

    public void TurnPage()
    {
        if(canTurn)
        {
            if (ActualPage < pages.Count)
            {
                canTurn = false;
                ActualPage -= 1;
                page = pages[ActualPage];
                StartCoroutine(TurnPageCoroutine(1));
                ActualPage += 2;
                
            }
            
        }
    }

    public void TurnPageBack()
    {
        if(canTurn)
        {
            if (ActualPage > 1)
            {
                canTurn = false;
                ActualPage -= 1;
                page = pages[ActualPage - 1];
                StartCoroutine(TurnPageCoroutine(-1));
                
            }
            
        }
    }

    IEnumerator TurnPageCoroutine(int direction)
    {
        float targetRotation = direction == 1 ? 91 : 0f;
        if(ActualPage == 0)
        {
            targetRotation = direction == 1 ? 180 : 0f;
        }
        float rotation = page.rotation.eulerAngles.y;

        while (Mathf.Abs(rotation - targetRotation) > 0.1f)
        {
            rotation = Mathf.MoveTowards(rotation, targetRotation, turnSpeed * Time.deltaTime);
            page.rotation = Quaternion.Euler(0f, rotation, 0f);
            yield return null;
        }

        // Reset canTurn after animation
        canTurn = true;
    }
}