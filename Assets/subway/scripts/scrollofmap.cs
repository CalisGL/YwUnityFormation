using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollmap : MonoBehaviour
{
    float speed = 1.0f;
    float distance = 5.0f;
    float elapsedTime = 0.0f;
    public bool YYY;

    void Update()
    {
        elapsedTime += Time.deltaTime * speed;
        if(YYY)
        {
            transform.position += new Vector3(0, 0, 1) * distance * Time.deltaTime;
        }
        else
        {
            transform.position += new Vector3(0, 1, 0) * distance * Time.deltaTime;
        }

        if (elapsedTime > 1.0f)
        {
            elapsedTime = 0.0f;
            if(YYY)
            {
                transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            }
            
        }
    }
}
