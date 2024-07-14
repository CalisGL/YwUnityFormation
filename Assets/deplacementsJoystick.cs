using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deplacementsJoystick : MonoBehaviour
{
    // Le joystick utilisé pour contrôler le joueur
    public string horizontalAxis = "Horizontal";
    public string verticalAxis = "Vertical";

    // La vitesse de déplacement du joueur
    public float speed = 5.0f;

    private Rigidbody rb;

    void Start()
    {
        // Récupère le composant Rigidbody du joueur
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Récupère les valeurs du joystick
        float horizontalInput = Input.GetAxis(horizontalAxis);
        float verticalInput = Input.GetAxis(verticalAxis);

        // Calcule la direction de déplacement
        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);

        // Applique la direction de déplacement au joueur
        rb.AddForce(movement * speed, ForceMode.VelocityChange);
    }
}
