using UnityEngine;

public class PauseGame : MonoBehaviour
{
    private bool isPaused = false;
    public DeplacementSimple joueur;

    // Met en pause ou reprend le jeu lorsque la touche spécifiée est enfoncée
    public void Pause()
    {
        Time.timeScale = 0f; // Arrête le temps dans le jeu
        isPaused = true;
        joueur.marcheAllowed(false);
    }

    // Reprend le jeu
    public void ResumeGame()
    {
        Time.timeScale = 1f; // Rétablit le temps normal dans le jeu
        isPaused = false;
        joueur.marcheAllowed(true);
    }

    public void pauseSystem()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            Pause();
        }
    }
}