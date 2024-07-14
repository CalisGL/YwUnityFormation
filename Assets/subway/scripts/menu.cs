using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class menu : MonoBehaviour
{
    public int bestScoreValue;
    public TextMeshProUGUI bestScoreObject;
    public int scoreValue;
    public TextMeshProUGUI scoreObject;
    public bool score;
    public TextMeshProUGUI pieces;
    public TextMeshProUGUI piecesTotal;
    public bool piecesBool;
    public Button boutonBingo;
    // Start is called before the first frame update
    void Start()
    {
        bestScoreValue = PlayerPrefs.GetInt("BestScore", 0);
        bestScoreObject.text = bestScoreValue.ToString();
        if(score)
        {
            scoreValue = PlayerPrefs.GetInt("Score", 0);
            scoreObject.text = scoreValue.ToString();
            if(piecesBool)
            {
                pieces.text = PlayerPrefs.GetInt("Pieces", 0).ToString();
            }
            piecesTotal.text = PlayerPrefs.GetInt("PiecesTotal", 0).ToString();
        }

        if (PlayerPrefs.GetInt("PiecesTotal", 0) >= 30)
        {
            if(boutonBingo != null)
            {
                boutonBingo.interactable = true;
            }
        }
        else
        {
            if(boutonBingo != null)
            {
                boutonBingo.interactable = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        piecesTotal.text = PlayerPrefs.GetInt("PiecesTotal", 0).ToString();
    }

    public void play(int map)
    {
        SceneManager.LoadScene(map);
    }

    public void retirerPices(int nb)
    {
        PlayerPrefs.SetInt("PiecesTotal", PlayerPrefs.GetInt("PiecesTotal", 0) - nb);
        PlayerPrefs.Save();
    }

    public void reset()
    {
        bestScoreValue = 0;
        PlayerPrefs.SetInt("BestScore", bestScoreValue);
        PlayerPrefs.Save();
        bestScoreObject.text = "Record : " + bestScoreValue.ToString();
    }

    public void resetGame()
    {
        PlayerPrefs.DeleteAll();
    }
}
