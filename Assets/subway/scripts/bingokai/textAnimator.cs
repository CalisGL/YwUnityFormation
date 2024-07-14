using UnityEngine;
using TMPro;

public class textAnimator : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public float delayBetweenCharacters = 0.1f;
    public bool activateSwitchWhenComplete = true;
    public GameObject switchObject;

    private bool isAnimating = false;
    private bool switchActivated = false;

    void Start()
    {
        if (textMeshPro == null)
        {
            Debug.LogError("TextMeshPro reference is not set!");
            return;
        }

        textMeshPro.maxVisibleCharacters = 0;
        isAnimating = true;
    }

    void Update()
    {
        if (isAnimating)
        {
            int visibleCharacters = textMeshPro.maxVisibleCharacters;

            if (visibleCharacters < textMeshPro.text.Length)
            {
                visibleCharacters++;
                textMeshPro.maxVisibleCharacters = visibleCharacters;
                Invoke("AnimateNextCharacter", delayBetweenCharacters);
            }
            else
            {
                isAnimating = false;
                if (activateSwitchWhenComplete && !switchActivated && switchObject != null)
                {
                    switchObject.SetActive(true);
                    switchActivated = true;
                }
            }
        }
    }

    void AnimateNextCharacter()
    {
        isAnimating = true;
    }
}
