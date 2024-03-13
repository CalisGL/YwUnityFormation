using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class data : MonoBehaviour
{
    List<string> yokaiPossedes = new List<string>();
    public string actualYokai = "y152000";
    public GameObject rotate;
    public GameObject actualGameObject;
    // Start is called before the first frame update
    void Start()
    {
        actualGameObject = rotate.transform.Find(actualYokai)?.gameObject;

        foreach (Transform child in rotate.transform)
        {
            if (child.gameObject != actualGameObject)
            {
                child.gameObject.SetActive(false);
            }
            actualGameObject.SetActive(true); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        //faire en sorte que tous les enfants de rotate à part actualGameObject soient désactivés
    }
}
