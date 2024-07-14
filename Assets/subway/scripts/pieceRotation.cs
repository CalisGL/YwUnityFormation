using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pieceRotation : MonoBehaviour
{
    // The speed at which the object will rotate
    public float rotationSpeed = 50.0f;

    // Update is called once per frame
    void Update()
    {
        // Rotate the object around the y-axis
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}