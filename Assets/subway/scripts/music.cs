using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class music : MonoBehaviour
{
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource.volume = PlayerPrefs.GetFloat("musique", 1f);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
