using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class score : MonoBehaviour
{
    public int scoreValue = 0;
    public float delay = 0.1f;
    private bool isScoring = false;
    public TextMeshProUGUI textMeshProUI;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AutoScore()); // Démarre la coroutine AutoScore au démarrage
    }

    void Update()
    {
        if (textMeshProUI != null) // Vérifie si la référence au TextMeshProUGUI est valide
        {
            textMeshProUI.text = scoreValue.ToString(); // Met à jour le texte avec la valeur du score
        }
    }
    // Coroutine pour augmenter automatiquement le score avec un délai
    IEnumerator AutoScore()
    {
        while (true) // Boucle infinie pour maintenir l'incrémentation du score
        {
            yield return new WaitForSeconds(delay); // Attendre le délai spécifié
            AugmenterScore(1); // Augmenter le score
        }
    }

    // Méthode pour augmenter le score
    public void AugmenterScore(int bonus)
    {
        scoreValue += bonus;
    }
}
