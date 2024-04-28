using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollmap : MonoBehaviour
{
    float speed = 1.0f;
    float distance = 5.0f;
    float elapsedTime = 0.0f;

    void Update()
    {
        elapsedTime += Time.deltaTime * speed;
        transform.position += new Vector3(0, 0, 1) * distance * Time.deltaTime;

        if (elapsedTime > 1.0f)
        {
            elapsedTime = 0.0f;
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }
}
