using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class parametres : MonoBehaviour
{
    public bool outline = true;
    public AudioSource audioSource;
    public Slider slider;
    public Toggle toggle;

    // Start is called before the first frame update
    void Start()
    {
        if (slider != null)
        {
            slider.value = PlayerPrefs.GetFloat("musique", 1f);
        }

        if (toggle != null)
        {
            if(PlayerPrefs.GetInt("outline", 1) == 1)
            {
                toggle.isOn  = true;
            }
            else {
                toggle.isOn  = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(audioSource != null)
        {
            audioSource.volume = PlayerPrefs.GetFloat("musique", 1f);
        }
    }

    public void outlineVoid(bool isOutline)
    {
        if(isOutline == false)
        {
            PlayerPrefs.SetInt("outline", 0);
        }
        else
        {
            PlayerPrefs.SetInt("outline", 1);
        }
        PlayerPrefs.Save();
    }

    public void musicVoid(float musicValue)
    {
        PlayerPrefs.SetFloat("musique", musicValue);
        PlayerPrefs.Save();
    }

    public void clearData()
    {
        PlayerPrefs.DeleteAll();
    }
}
