using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public int voie = 2;
    public float voie1 = -0.75f;
    public float voie2 = 0f;
    public float voie3 = 0.75f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(voie == 2)
            {
                voie = 1;
                transform.position = new Vector3(voie1, transform.position.y, transform.position.z);
            }
            else if(voie == 3)
            {
                voie = 2;
                transform.position = new Vector3(voie2, transform.position.y, transform.position.z);
            }
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(voie == 2)
            {
                voie = 3;
                transform.position = new Vector3(voie3, transform.position.y, transform.position.z);
            }
            else if(voie == 1)
            {
                voie = 2;
                transform.position = new Vector3(voie2, transform.position.y, transform.position.z);
            }
        }

        if(touchAnOpponent())
        {
            Debug.Log("Game Over !");
        }

    }

    bool touchAnOpponent()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.forward, out hit, 0.5f))
        {
            if (hit.collider.CompareTag("ennemi"))
            {
                return true; 
            }
        }
        return false;
    }
}
