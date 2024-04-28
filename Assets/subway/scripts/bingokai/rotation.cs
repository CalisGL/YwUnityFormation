using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rotation : MonoBehaviour
{
// Variable pour stocker la position initiale du toucher
    private Vector2 touchStart;
    public float seuil = 7f;
    public float seuilMax = 15f;
    public GameObject Filtre;
    public Image Fleche;
    public RectTransform CapsuleRectTransform; // Utilisez RectTransform pour manipuler les UI GameObjects
    public float vitesseDescente = 50f;
    public float positionCibleY = 160f;
    public float vitesseRotationCapsule = 100f;
    public float positionCibleY2 = 300f;
    bool roueTournee = false;
    public GameObject CapsuleHaut;
    public GameObject CapsuleBas;

    void Update() {
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase) {
                case TouchPhase.Began:
                    // Stocke la position initiale du toucher
                    touchStart = touch.position;
                    break;
                case TouchPhase.Moved:
                    // Calcule la différence entre la position actuelle et la position initiale
                    Vector2 touchEnd = touch.position;
                    float angle = Vector2.SignedAngle(touchStart, touchEnd);

                    // Si l'angle est suffisamment grand, déclenche l'action
                    if (Mathf.Abs(angle) > seuil && Mathf.Abs(angle) < seuilMax) { // Seuil d'angle à ajuster selon vos besoins
                        if(!roueTournee)
                        {
                            TriggerRotationAction();
                        }
                        roueTournee = true;
                        // Réinitialise la position initiale pour la prochaine rotation
                        touchStart = touch.position;
                    }
                    break;
            }
        }
    }


    IEnumerator DescendreCapsule()
    {
        while (CapsuleRectTransform.anchoredPosition.y >= positionCibleY)
        {
            // Faire descendre la capsule
            CapsuleRectTransform.anchoredPosition -= new Vector2(0, vitesseDescente * Time.deltaTime);

            // Faire tourner la capsule
            CapsuleRectTransform.Rotate(Vector3.forward, Time.deltaTime * vitesseRotationCapsule); // Ajustez la vitesse de rotation selon vos besoins
            
            yield return null;
        }
        StartCoroutine(Rebond());
    }

    IEnumerator Rebond()
    {
        while (CapsuleRectTransform.anchoredPosition.y <= positionCibleY2)
        {
            // Faire descendre la capsule
            CapsuleRectTransform.anchoredPosition -= new Vector2(0, vitesseDescente * Time.deltaTime * -1 / 4);

            // Faire tourner la capsule
            CapsuleRectTransform.Rotate(Vector3.forward, Time.deltaTime * vitesseRotationCapsule * -1); // Ajustez la vitesse de rotation selon vos besoins
            
            yield return null;
        }
        StartCoroutine(Rebond2());
        
    }

    IEnumerator Rebond2()
    {
        while (CapsuleRectTransform.anchoredPosition.y >= positionCibleY)
        {
            // Faire descendre la capsule
            CapsuleRectTransform.anchoredPosition -= new Vector2(0, vitesseDescente * Time.deltaTime / 4);

            // Faire tourner la capsule
            CapsuleRectTransform.Rotate(Vector3.forward, Time.deltaTime * vitesseRotationCapsule * -1); // Ajustez la vitesse de rotation selon vos besoins
            
            yield return null;
        }
        //essaye de la faire tourner d'un sens puis d'un autre un certain nombre de fois comme si le yo-kai qui était dans la capsule essayait de sortir
        StartCoroutine(YoKaiSort());
    }

    IEnumerator YoKaiSort()
    {
        // Nombre de fois que le Yo-kai va essayer de sortir
        int nombreDeTentatives = 3;

        for (int i = 0; i < nombreDeTentatives; i++)
        {
            // Faire tourner la capsule d'un sens
            for (int j = 0; j < 8; j++)
            {
                CapsuleRectTransform.Rotate(Vector3.forward, Time.deltaTime * vitesseRotationCapsule * 5);
                yield return null;
            }

            // Faire tourner la capsule dans l'autre sens
            for (int j = 0; j < 8; j++)
            {
                CapsuleRectTransform.Rotate(Vector3.forward, Time.deltaTime * -vitesseRotationCapsule * 5);
                yield return null;
            }
        }
        StartCoroutine(reorienter());
    }

    IEnumerator reorienter()
    {
        // Rotation finale souhaitée pour la capsule
        float rotationFinaleZ = 20f;

        while (CapsuleRectTransform.localEulerAngles.z == rotationFinaleZ)
        {
            // Faire tourner la capsule d'un sens
            CapsuleRectTransform.Rotate(Vector3.forward, Time.deltaTime * vitesseRotationCapsule);
            yield return null;
        }

        // Si la rotation finale est dépassée, ramener la rotation à la valeur exacte
        CapsuleRectTransform.localEulerAngles = new Vector3(0, 0, rotationFinaleZ);
        StartCoroutine(Eloignement());
    }

    IEnumerator Eloignement()
    {
        Vector2 positionCibleHaut = new Vector2(-308f, 568f);
        Vector2 positionCibleBas = new Vector2(296f, -503f);
        float vitesseDeplacement = 50f;

        CapsuleRectTransform.GetComponent<Image>().enabled = false;
        CapsuleHaut.SetActive(true);
        CapsuleBas.SetActive(true);
        while (Vector2.Distance(CapsuleHaut.transform.localPosition, positionCibleHaut) > 0.1f ||
            Vector2.Distance(CapsuleBas.transform.localPosition, positionCibleBas) > 0.1f)
        {
            // Déplacer la capsule du haut
            CapsuleHaut.transform.localPosition = Vector2.MoveTowards(CapsuleHaut.transform.localPosition, positionCibleHaut, vitesseDeplacement * Time.deltaTime * 20);

            // Déplacer la capsule du bas
            CapsuleBas.transform.localPosition = Vector2.MoveTowards(CapsuleBas.transform.localPosition, positionCibleBas, vitesseDeplacement * Time.deltaTime * 20);

            yield return null;
        }
    }
        

    public void TriggerRotationAction()
    {
        CapsuleRectTransform.anchoredPosition = new Vector2(40f, 1262f);
        Debug.Log("rotation");
        Filtre.SetActive(true);
        Fleche.enabled = false;
        StartCoroutine(DescendreCapsule());
    }
}
