using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangementScene : MonoBehaviour
{
    public KeyCode toucheChangementScene = KeyCode.Return; // Touche pour changer de scène
    public KeyCode toucheWorldmap = KeyCode.F; // Touche pour changer de scène
    public string nomDeLaScene = "NomDeLaScene"; // Nom de la scène à charger
    public int levelX = 1; // Numéro du niveau à charger
    public GameObject originObject;
    public GameObject canvaChoice;


    // Détermine si le joueur est en contact avec cet objet
    public bool joueurEnContact = false;
    public DeplacementSimple controller;

    // Update est appelé une fois par frame
    void Update()
    {
        // Met à jour l'état de joueurEnContact en fonction du résultat de la raycast
        joueurEnContact = contactPlayer();

        // Vérifie si la touche de changement de scène est enfoncée et si le joueur est en contact avec l'objet
        if (Input.GetKeyDown(toucheChangementScene) && joueurEnContact)
        {
            controller.walkDisabled = true;
            canvaChoice.SetActive(true);
        }
        if (Input.GetKeyDown(toucheWorldmap))
        {
            LoadWorldmap();
        }
    }

    bool contactPlayer()
    {
        if (originObject != null)
        {
            Vector3 originPosition = originObject.transform.position;
            Vector3 direction = originObject.transform.forward;

            RaycastHit hit;
            
            if (Physics.Raycast(originPosition, direction, out hit, 0.5f))
            {
                return hit.collider.gameObject == gameObject;
            }
        }
        else
        {
            Debug.LogWarning("L'objet d'origine n'est pas défini !");
        }

        return false;
    }

    public void LoadLevel(int level)
    {
        string levelName = "level" + level.ToString();
        SceneManager.LoadScene(levelName);
        controller.walkDisabled = true;
    }

    public void LoadWorldmap()
    {
        SceneManager.LoadScene("worldmap");
    }

    public void Annuler()
    {
        canvaChoice.SetActive(false);
        controller.walkDisabled = false;
    }
}
