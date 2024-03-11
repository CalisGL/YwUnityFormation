using UnityEngine;

public class animationYokai : MonoBehaviour
{
    public DeplacementSimple joueur; // Référence au script playerController attaché au joueur
    public Animator animator; // Référence à l'Animator du joueur

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Assurez-vous que l'animator a bien été récupéré
        if (animator != null)
        {
            // Mettre à jour la variable animationYokai de l'Animator
            animator.SetInteger("Anim", joueur.animationYokai);
        }
    }
}
