using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseGameRun : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pause()
    {
        // Mettre la vitesse du temps à 0 pour mettre le jeu en pause
        Time.timeScale = 0f;
    }

    public void unpause()
    {
        // Mettre la vitesse du temps à 0 pour mettre le jeu en pause
        Time.timeScale = 1f;
    }

    public void quitter()
    {
        SceneManager.LoadScene(2);
    }
}
