using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public Image Flash;
    float couleur = 0f;
    bool alreadyload = false;
    AsyncOperation operation;
    bool alreadyFlash = false;
    public Image CapsuleGUI;
    public Image CapsuleHautS;
    public Image CapsuleBasS;
    public yokais yokais;
    public string IDyokai;
    string rang = "";
    List<string> yokaiDispo;

    void Start()
    {
        SaveYokaiListToPlayerPrefs(0, "yokaiPossE");
    }
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
        StartCoroutine(RangYokai());

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
            if((Vector2.Distance(CapsuleHaut.transform.localPosition, positionCibleHaut) > 0.05f ||
            Vector2.Distance(CapsuleBas.transform.localPosition, positionCibleBas) > 0.05f) && !alreadyFlash)
            {
                StartCoroutine(flash());
                alreadyFlash = true;
            }
            // Déplacer la capsule du bas
            CapsuleBas.transform.localPosition = Vector2.MoveTowards(CapsuleBas.transform.localPosition, positionCibleBas, vitesseDeplacement * Time.deltaTime * 20);

            yield return null;
        }

    }

    IEnumerator flash()
    {   
        while (couleur < 0.7 )
        {
            couleur += 0.01f;
            Flash.color = new Color(1f, 1f, 1f, couleur);
            yield return new WaitForSeconds(0.01f);
        }
        PlayerPrefs.SetInt("BingoPlay", 1);
        if(!alreadyload)
        {
            operation = SceneManager.LoadSceneAsync(3);
            alreadyload= true;
        }
        
        while (couleur < 1 && !operation.isDone)
        {
            couleur += 0.01f;
            Flash.color = new Color(1f, 1f, 1f, couleur);
            yield return new WaitForSeconds(0.01f);
        }
        //SceneManager.LoadScene(3);
    }

    IEnumerator RangYokai()
    {
        // Générer un nombre aléatoire entre 1 et 100
        int randomNumber = UnityEngine.Random.Range(1, 101);


        if(randomNumber == 100)
        {
            RangS();
        }
        else if(randomNumber >= 1 && randomNumber <= 5)
        {
            RangA();
        }
        else if(randomNumber >= 6 && randomNumber <= 13)
        {
            RangB();
        }
        else if(randomNumber >= 14 && randomNumber <= 28)
        {
            RangC();
        }
        else if(randomNumber >= 29 && randomNumber <= 53)
        {
            RangD();
        }
        else
        {
            RangE();
        }
        Debug.Log("rang : " + rang);
        PlayerPrefs.SetString("IDyokai", IDyokai);
        yield return null;
    }
        

    public void TriggerRotationAction()
    {
        CapsuleRectTransform.anchoredPosition = new Vector2(40f, 1262f);
        Debug.Log("rotation");
        Filtre.SetActive(true);
        Fleche.enabled = false;
        StartCoroutine(DescendreCapsule());
    }

    public void SaveYokaiListToPlayerPrefs(int ListID, string nameOfList)
    {
        //yokais.Yokai[1][0].Add("y101000");

        // Sérialiser la liste en une chaîne JSON
        //string json = JsonUtility.ToJson(yokais.Yokai[1][ListID]);

        // Sauvegarder la chaîne JSON dans PlayerPrefs
        //PlayerPrefs.SetString(nameOfList, json);
        //PlayerPrefs.Save();

        //Debug.Log("Saved Yokai List to PlayerPrefs: " + nameOfList);

        string listAsString = string.Join(",", yokais.Yokai[1][ListID]);

        // Sauvegarder la chaîne dans PlayerPrefs
        PlayerPrefs.SetString(nameOfList, listAsString);
        PlayerPrefs.Save();

        Debug.Log("Saved Yokai List to PlayerPrefs: " + nameOfList);
    }

    public void RangE()
    {
        rang = "E";
        Sprite spriteEntier = Resources.Load<Sprite>("BlancEntier");
        Sprite spriteHaut = Resources.Load<Sprite>("BlancHaut");
        Sprite spriteBas = Resources.Load<Sprite>("BlancBas");
        CapsuleGUI.sprite = spriteEntier;
        CapsuleHautS.sprite = spriteHaut;
        CapsuleBasS.sprite = spriteBas;
        yokaiDispo = yokais.Yokai[2][0];
        if(yokaiDispo.Count > 0)
        {
            int randomYokai = UnityEngine.Random.Range(0, yokaiDispo.Count);
            IDyokai = yokaiDispo[randomYokai];
            yokais.Yokai[1][0].Add(IDyokai);
            SaveYokaiListToPlayerPrefs(0, "yokaiPossE");
        }
        else {
            RangD();
        }
    }

    public void RangD()
    {
        rang = "D";
        Sprite spriteEntier = Resources.Load<Sprite>("BleuEntier");
        Sprite spriteHaut = Resources.Load<Sprite>("BleuHaut");
        Sprite spriteBas = Resources.Load<Sprite>("BleuBas");
        CapsuleGUI.sprite = spriteEntier;
        CapsuleHautS.sprite = spriteHaut;
        CapsuleBasS.sprite = spriteBas;
        yokaiDispo = yokais.Yokai[2][1];
        if(yokaiDispo.Count > 0)
        {
            int randomYokai = UnityEngine.Random.Range(0, yokaiDispo.Count);
            IDyokai = yokaiDispo[randomYokai];
            yokais.Yokai[1][1].Add(IDyokai);
            SaveYokaiListToPlayerPrefs(1, "yokaiPossD");
        }
        else {
            RangC();
        }
    }

    public void RangC()
    {
        rang = "C";
        Sprite spriteEntier = Resources.Load<Sprite>("BleuEntier");
        Sprite spriteHaut = Resources.Load<Sprite>("BleuHaut");
        Sprite spriteBas = Resources.Load<Sprite>("BleuBas");
        CapsuleGUI.sprite = spriteEntier;
        CapsuleHautS.sprite = spriteHaut;
        CapsuleBasS.sprite = spriteBas;
        yokaiDispo = yokais.Yokai[2][2];
        if(yokaiDispo.Count > 0)
        {
            int randomYokai = UnityEngine.Random.Range(0, yokaiDispo.Count);
            IDyokai = yokaiDispo[randomYokai];
            yokais.Yokai[1][2].Add(IDyokai);
            SaveYokaiListToPlayerPrefs(2, "yokaiPossC");
        }
        else {
            RangB();
        }
    }

    public void RangB()
    {
        rang = "B";
        Sprite spriteEntier = Resources.Load<Sprite>("RougeEntier");
        Sprite spriteHaut = Resources.Load<Sprite>("RougeHaut");
        Sprite spriteBas = Resources.Load<Sprite>("RougeBas");
        CapsuleGUI.sprite = spriteEntier;
        CapsuleHautS.sprite = spriteHaut;
        CapsuleBasS.sprite = spriteBas;
        yokaiDispo = yokais.Yokai[2][3];
        if(yokaiDispo.Count > 0)
        {
            int randomYokai = UnityEngine.Random.Range(0, yokaiDispo.Count);
            IDyokai = yokaiDispo[randomYokai];
            yokais.Yokai[1][3].Add(IDyokai);
            SaveYokaiListToPlayerPrefs(3, "yokaiPossB");
        }
        else {
            RangA();
        }
    }

    public void RangA()
    {
        rang = "A";
        Sprite spriteEntier = Resources.Load<Sprite>("RougeEntier");
        Sprite spriteHaut = Resources.Load<Sprite>("RougeHaut");
        Sprite spriteBas = Resources.Load<Sprite>("RougeBas");
        CapsuleGUI.sprite = spriteEntier;
        CapsuleHautS.sprite = spriteHaut;
        CapsuleBasS.sprite = spriteBas;
        yokaiDispo = yokais.Yokai[2][4];
        if(yokaiDispo.Count > 0)
        {
            int randomYokai = UnityEngine.Random.Range(0, yokaiDispo.Count);
            IDyokai = yokaiDispo[randomYokai];
            yokais.Yokai[1][4].Add(IDyokai);
            SaveYokaiListToPlayerPrefs(4, "yokaiPossA");
        }
        else {
            RangS();
        }
    }

    public void RangS()
    {
        rang = "S";
        Sprite spriteEntier = Resources.Load<Sprite>("OrEntier");
        Sprite spriteHaut = Resources.Load<Sprite>("OrHaut");
        Sprite spriteBas = Resources.Load<Sprite>("OrBas");
        CapsuleGUI.sprite = spriteEntier;
        CapsuleHautS.sprite = spriteHaut;
        CapsuleBasS.sprite = spriteBas;
        yokaiDispo = yokais.Yokai[2][5];
        if(yokaiDispo.Count > 0)
        {
            int randomYokai = UnityEngine.Random.Range(0, yokaiDispo.Count);
            IDyokai = yokaiDispo[randomYokai];
            yokais.Yokai[1][5].Add(IDyokai);
            SaveYokaiListToPlayerPrefs(5, "yokaiPossS");
        }
        else {
            RangE();
        }
    }
}