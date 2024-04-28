using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nuages : MonoBehaviour
{
    public bool droite;
    public float avancement = 0f;
    public float vitesse = 1f;
    public int limite = 300;
    public bool lineaire = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lineaire)
        {
            if (droite)
            {
                transform.Translate(Vector3.right * vitesse);
                avancement += 1;
                if(avancement >= limite)
                {
                    droite = false;
                    avancement = 0;
                }
            }
            else
            {
                transform.Translate(Vector3.left * vitesse);
                avancement += 1;
                if(avancement >= limite)
                {
                    droite = true;
                    avancement = 0;
                }
            }
        }
        else
        {
            transform.Rotate(Vector3.forward * vitesse);
        }
    }
}
