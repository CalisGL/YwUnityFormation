using UnityEngine;

public class DeplacementSimple : MonoBehaviour
{
    public float vitesseDeplacementMarche = 5f; // Vitesse de déplacement en marchant
    public float vitesseDeplacementCourse = 10f; // Vitesse de déplacement en courant
    public float puissanceSaut = 5f;
    public KeyCode toucheCourse = KeyCode.LeftShift; // Touche pour courir
    public KeyCode toucheSaut = KeyCode.Space; // Touche pour sauter
    public GameObject rotate;
    public int animationYokai; // Variable pour stocker l'animation à jouer

    private bool enMarche; // Indique si le personnage marche
    private bool enCourse; // Indique si le personnage court
    private float vitesseDeplacement = 5f;
    private Rigidbody rb; // Référence au Rigidbody attaché au joueur

    void Start()
    {
        animationYokai = 0; // Au début, aucune animation
        rb = GetComponent<Rigidbody>(); // Récupérer le Rigidbody
    }

    void Update()
    {
        // Si la touche de course est enfoncée, utiliser la vitesse de course
        if (Input.GetKey(toucheCourse) && enMarche == true)
        {
            enCourse = true;
            vitesseDeplacement = vitesseDeplacementCourse;
        }
        else
        {
            enCourse = false;
            vitesseDeplacement = vitesseDeplacementMarche;
        }

        // Déplacement vers l'avant
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.forward * vitesseDeplacement * Time.deltaTime);
            rotate.transform.rotation = Quaternion.Euler(0f, 0f, 0f); // Rotation dans l'axe y = 0
            enMarche = true;
        }
        else if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.A)) // Déplacement vers l'arrière
        {
            transform.Translate(Vector3.back * vitesseDeplacement * Time.deltaTime);
            rotate.transform.rotation = Quaternion.Euler(0f, 180f, 0f); // Rotation dans l'axe y = 180
            enMarche = true;
        }
        else
        {
            enMarche = false;
        }

        // Saut
        if (Input.GetKeyDown(toucheSaut) && ToucheUnJumper())
        {
            Jump();
        }

        // Déterminer quelle animation doit être jouée
        if (enCourse)
        {
            animationYokai = 2; // Animation de course
        }
        else if (enMarche)
        {
            animationYokai = 1; // Animation de marche
        }
        else
        {
            animationYokai = 0; // Aucune animation
        }
    }

    void Jump()
    {
        // Assurez-vous que le joueur a un Rigidbody attaché
        if (rb != null)
        {
            // Appliquer une force vers le haut pour simuler le saut
            rb.AddForce(Vector3.up * puissanceSaut, ForceMode.Impulse);
        }
        else
        {
            Debug.LogWarning("Rigidbody manquant sur le joueur pour effectuer le saut.");
        }
    }

    bool ToucheUnJumper()
    {
        // Raycast vers le bas pour détecter un objet avec le tag "jumper"
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.05f)) // Ajustez le rayon si nécessaire
        {
            if (hit.collider.CompareTag("jumper"))
            {
                return true;
            }
        }
        return false;
    }
}
