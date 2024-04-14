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
    public GameObject onigiri;
    public bool onigiriTook;
    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered
    
    // Start is called before the first frame update
    void Start()
    {
        dragDistance = Screen.height * 15 / 100;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moveLeft();
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveRight();
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

        if(touchAnOnigiri())
        {
            if(vies < 3 && !onigiriTook)
            {
                vies += 1;
            }
            onigiri.transform.position = new Vector3(transform.position.x, transform.position.y, -2);
            onigiriTook = true;
            StartCoroutine(onigiriTaken());
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


        if (Input.touchCount == 1) // user is touching the screen with a single touch
        {
            Touch touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                lp = touch.position;  //last touch position. Ommitted if you use list
 
                //Check if drag distance is greater than 20% of the screen height
                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                {//It's a drag
                 //check if the drag is vertical or horizontal
                    if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                    {   //If the horizontal movement is greater than the vertical movement...
                        if ((lp.x > fp.x))  //If the movement was to the right)
                        {   //Right swipe
                            Debug.Log("Right Swipe");
                            moveRight();
                        }
                        else
                        {   //Left swipe
                            Debug.Log("Left Swipe");
                            moveLeft();
                        }
                    }
                    else
                    {   //the vertical movement is greater than the horizontal movement
                        if (lp.y > fp.y)  //If the movement was up
                        {   //Up swipe
                            Debug.Log("Up Swipe");
                        }
                        else
                        {   //Down swipe
                            Debug.Log("Down Swipe");
                        }
                    }
                }
                else
                {   //It's a tap as the drag distance is less than 20% of the screen height
                    Debug.Log("Tap");
                }
            }
        }
    }

    void moveRight()
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

    void moveLeft()
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

    bool touchAnOnigiri()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.forward, out hit, 0.5f))
        {
            if (hit.collider.CompareTag("onigiri"))
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

    IEnumerator onigiriTaken()
    {
        yield return new WaitForSeconds(tempsInvicibilite);
        onigiriTook = false;
    }

    public void gameover()
    {
        SceneManager.LoadScene(0);
    }
}
