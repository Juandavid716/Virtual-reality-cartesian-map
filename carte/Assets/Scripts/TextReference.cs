using UnityEngine;
using System.Collections;
using TMPro; // Add the TextMesh Pro namespace to access the various functions.


public class TextReference : MonoBehaviour
{

    void Update()
    {
        Vector3 camPos = Camera.main.transform.position;

        transform.LookAt(
            new Vector3(-camPos.x, transform.position.y, -camPos.z)
            );

    }
   
}