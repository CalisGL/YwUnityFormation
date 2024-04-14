using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class move : MonoBehaviour
{
    public int voie = 2;
    public float voie1 = -0.75f;
    public float voie2 = 0f;
    public float voie3 = 0.75f;
    public int vies = 3;
    public bool invincible;
    public float tempsInvicibilite = 2f;
    public GameObject vie1;
    public GameObject vie2;
    public GameObject vie3;
    
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
            if(vies < 0)
            {
                //gameover
            }
            else
            {
                vies -= 1;
            }
            invincible = true;
            StartCoroutine(InvincibilityTimer()); // Démarre le timer d'invincibilité
        }

        if(vies == 3)
        {
            vie1.SetActive(true);
            vie2.SetActive(true);
            vie3.SetActive(true);
        }
        else if(vies == 2)
        {
            vie1.SetActive(true);
            vie2.SetActive(true);
            vie3.SetActive(false);
        }
        else if(vies == 1)
        {
            vie1.SetActive(true);
            vie2.SetActive(false);
            vie3.SetActive(false);
        }
        else if(vies <= 0)
        {
            vie1.SetActive(false);
            vie2.SetActive(false);
            vie3.SetActive(false);
        }

        if(vies < 0)
        {
            gameover();
        }

    }

    bool touchAnOpponent()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.forward, out hit, 0.5f))
        {
            if (hit.collider.CompareTag("ennemi") && !invincible)
            {
                return true; 
            }
        }
        return false;
    }

    IEnumerator InvincibilityTimer()
    {
        yield return new WaitForSeconds(tempsInvicibilite);
        invincible = false;
    }

    public void gameover()
    {
        SceneManager.LoadScene(0);
    }
}
