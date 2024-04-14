using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveEnnemi : MonoBehaviour
{
    public float vitesse = 1f;
    public int voie;
    public float pointDeSpawn = 8f;

    void Start()
    {
        Spawn(true);
    }
    // Update is called once per frame
    void Update()
    {
        // Déplacement vers l'avant (direction positive de l'axe Z)
        transform.Translate(Vector3.forward * vitesse * Time.deltaTime);

        // Vérifie si la position x est inférieure à -2
        if (transform.position.z < -2f)
        {
            // Appelle la fonction pour réinitialiser la position
            Spawn(false);
        }
    }

    void Spawn(bool debut)
    {
        // Génère un nombre aléatoire entre 1 et 3 pour choisir la voie
        voie = Random.Range(1, 4);

        if(!debut)
        {
            if (voie == 1)
            {
                transform.position = new Vector3(-0.75f, transform.position.y, pointDeSpawn);
            }
            else if (voie == 2)
            {
                transform.position = new Vector3(0f, transform.position.y, pointDeSpawn);
            }
            else if (voie == 3)
            {
                transform.position = new Vector3(0.75f, transform.position.y, pointDeSpawn);
            }
        }
        else
        {
            if (voie == 1)
            {
                transform.position = new Vector3(-0.75f, transform.position.y, transform.position.z);
            }
            else if (voie == 2)
            {
                transform.position = new Vector3(0f, transform.position.y, transform.position.z);
            }
            else if (voie == 3)
            {
                transform.position = new Vector3(0.75f, transform.position.y, transform.position.z);
            }
        }
    }
}
