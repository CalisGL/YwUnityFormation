using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollofmap : MonoBehaviour
{
    public float speed = 1.0f;
    public float distance = 5.0f;
    public float elapsedTime = 0.0f;
    public float time = 3f;

    void Update()
    {
        elapsedTime += Time.deltaTime * speed;
        transform.position += new Vector3(0, 0, 1) * distance * Time.deltaTime;

        if (elapsedTime > time)
        {
            elapsedTime = 0.0f;
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }
}
