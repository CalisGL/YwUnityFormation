using UnityEngine;

public class playerControllerWorldmap : MonoBehaviour
{
    public float vitesseDeplacement = 5f; // Vitesse de déplacement
    public float puissanceSaut = 5f; // Puissance du saut
    public KeyCode toucheSaut = KeyCode.Space; // Touche pour sauter
    public GameObject rotate; // Objet à faire pivoter
    public bool worldmap = false; // Indicateur de monde
    public float joystickSensitivity = 0.1f; // Sensibilité du joystick

    private Rigidbody rb; // Référence au Rigidbody attaché au joueur
    private Vector3 movementDirection; // Direction du mouvement

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Récupérer le Rigidbody
    }

    void Update()
    {
        // Déplacement du personnage en fonction de la direction du joystick
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        movementDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // Saut
        if (Input.GetKeyDown(toucheSaut) && ToucheUnJumper())
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        // Déplacer le personnage en fonction de la direction du joystick
        MoveCharacter(movementDirection);
    }

    void MoveCharacter(Vector3 direction)
    {
        if (direction.magnitude >= joystickSensitivity)
        {
            // Rotation du personnage
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            rotate.transform.rotation = Quaternion.Lerp(rotate.transform.rotation, targetRotation, 0.1f);

            // Déplacement du personnage
            Vector3 moveDirection = rotate.transform.forward * vitesseDeplacement * Time.fixedDeltaTime;
            rb.MovePosition(transform.position + moveDirection);
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
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.1f)) // Ajustez le rayon si nécessaire
        {
            if (hit.collider.CompareTag("jumper"))
            {
                return true;
            }
        }
        return false;
    }
}
