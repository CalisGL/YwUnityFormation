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
    public score score;
    public bool pieceTook;
    public GameObject piece;
    public int pieces;
    public float moveSpeed = 5f; // Vitesse de déplacement
    public bool testmode = false;
    private Vector3 targetPosition; // La position cible du déplacement
    private bool isMoving = false; // Indique si le déplacement est en cours
    
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.DeleteAll();
        dragDistance = Screen.height * 5 / 100;
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

        if (isMoving)
        {
            // Déplace l'objet vers la position cible
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Vérifie si l'objet est arrivé à la position cible
            if (transform.position == targetPosition)
            {
                isMoving = false; // Fin du déplacement
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
                if(!testmode)
                {
                    vies -= 1;
                }
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
            onigiri.transform.position = new Vector3(transform.position.x, transform.position.y, -5);
            onigiriTook = true;
            StartCoroutine(onigiriTaken());
        }
        if(touchAPiece())
        {
            if(!pieceTook)
            {
                float nbPiece = 1 + (score.scoreValue / 500);
                int nbPieceInt = Mathf.RoundToInt(nbPiece); // Utilisez Mathf.RoundToInt pour arrondir un float à un entier
                pieces += nbPieceInt; 
            }
            piece.transform.position = new Vector3(transform.position.x, transform.position.y, -5);
            pieceTook = true;
            StartCoroutine(pieceTaken());
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

    public void moveRight()
    {
        if (!isMoving) // Vérifie si un déplacement n'est pas déjà en cours
        {
            if (voie == 2)
            {
                targetPosition = new Vector3(voie3, transform.position.y, transform.position.z);
                voie = 3;
                isMoving = true; // Démarre le déplacement
            }
            else if (voie == 1)
            {
                targetPosition = new Vector3(voie2, transform.position.y, transform.position.z);
                voie = 2;
                isMoving = true; // Démarre le déplacement
            }
        }
    }

    // Fonction pour déplacer vers la gauche
    public void moveLeft()
    {
        if (!isMoving) // Vérifie si un déplacement n'est pas déjà en cours
        {
            if (voie == 2)
            {
                targetPosition = new Vector3(voie1, transform.position.y, transform.position.z);
                voie = 1;
                isMoving = true; // Démarre le déplacement
            }
            else if (voie == 3)
            {
                targetPosition = new Vector3(voie2, transform.position.y, transform.position.z);
                voie = 2;
                isMoving = true; // Démarre le déplacement
            }
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

    bool touchAPiece()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.forward, out hit, 0.5f))
        {
            if (hit.collider.CompareTag("pièce 1"))
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

    IEnumerator pieceTaken()
    {
        yield return new WaitForSeconds(tempsInvicibilite);
        pieceTook = false;
    }

    public void gameover()
    {
        PlayerPrefs.SetInt("Pieces", pieces);
        PlayerPrefs.SetInt("PiecesTotal", pieces + PlayerPrefs.GetInt("PiecesTotal", 0));
        PlayerPrefs.SetInt("Score", score.scoreValue);
        PlayerPrefs.Save();
        SceneManager.LoadScene(2);
    }
}
