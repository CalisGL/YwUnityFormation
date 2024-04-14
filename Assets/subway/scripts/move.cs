using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public int voie = 2;
    public float voie1 = -0.75f;
    public float voie2 = 0f;
    public float voie3 = 0.75f;
    public bool invicible = false;

    private Vector2 touchStartPos;
    private bool isSwipe = false;

    // Update is called once per frame
    void Update()
    {
        // Détection du swipe
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStartPos = touch.position;
                    isSwipe = true;
                    break;
                case TouchPhase.Moved:
                    break;
                case TouchPhase.Ended:
                    if (isSwipe)
                    {
                        float swipeDistX = (touch.position.x - touchStartPos.x);
                        if (Mathf.Abs(swipeDistX) > 50f) // Sensibilité du swipe
                        {
                            if (swipeDistX > 0) // Swipe vers la droite
                            {
                                MoveRight();
                            }
                            else // Swipe vers la gauche
                            {
                                MoveLeft();
                            }
                        }
                    }
                    break;
            }
        }
    }

    void MoveLeft()
    {
        if (voie == 2)
        {
            voie = 1;
            transform.position = new Vector3(voie1, transform.position.y, transform.position.z);
        }
        else if (voie == 3)
        {
            voie = 2;
            transform.position = new Vector3(voie2, transform.position.y, transform.position.z);
        }
    }

    void MoveRight()
    {
        if (voie == 2)
        {
            voie = 3;
            transform.position = new Vector3(voie3, transform.position.y, transform.position.z);
        }
        else if (voie == 1)
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
            if (hit.collider.CompareTag("ennemi"))
            {
                return true; 
            }
        }
        return false;
    }
}
