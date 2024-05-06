using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class flashEnd : MonoBehaviour
{
    public Image flashImage;
    float colorAlpha = 1f;
    public GameObject joueur;
    public GameObject fond;
    public GameObject back;
    int BingoPlay;
    public GameObject OK;
    public GameObject scaler;
    void Start()
    {
        BingoPlay = PlayerPrefs.GetInt("BingoPlay", 0);
        if (BingoPlay == 1)
        {
            flashImage.color = new Color(1f, 1f, 1f, 1f);
            StartCoroutine(FlashEffect());
            fond.SetActive(true);
            joueur.SetActive(true);
            back.SetActive(false);
            string IDyokai = PlayerPrefs.GetString("IDyokai", "y001000");
            //activer l'enfant appelé "y001000" de l'objet "joueur"
            Debug.Log(IDyokai);
            scaler.transform.Find(IDyokai).gameObject.SetActive(true);
            Debug.Log(IDyokai);
            OK.SetActive(true);

        }
        else
        {
            flashImage.gameObject.SetActive(false);
            fond.SetActive(false);
            joueur.SetActive(false);
            OK.SetActive(false);
        }
        PlayerPrefs.SetInt("BingoPlay", 0);
    }

    IEnumerator FlashEffect()
    {
        while (colorAlpha >= 0)
        {
            colorAlpha -= 0.1f;
            flashImage.color = new Color(1f, 1f, 1f, colorAlpha);
            yield return new WaitForSeconds(0.01f);
        }
        flashImage.gameObject.SetActive(false);
    }

    public void Quit()
    {
        PlayerPrefs.SetInt("BingoPlay", 0); // 0 for false, 1 for true
    }
}
