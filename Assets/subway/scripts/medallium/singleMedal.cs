using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class singleMedal : MonoBehaviour
{
    public medallium medallium;
    Image medal;
    public string ID;
    public playerSkinMedallium playerSkinMedallium;
    
    // Start is called before the first frame update
    void Start()
    {
        medal = transform.GetChild(0).GetComponent<Image>();
        ID = medal.sprite.name.Substring(0, 7);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if medallium.yokai contains the element ID
        if (medallium.yokai.Contains(ID) && medal != null)
        {
            medal.enabled = true;
        }
        else
        {
            medal.enabled = false;
        }
    }

    public void changeYokai()
    {
        playerSkinMedallium.ID = ID;
    }
}
