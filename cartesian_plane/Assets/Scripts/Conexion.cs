using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Conexion : MonoBehaviour
{
    GameObject object1;
    GameObject object2;
    // Use this for initialization
    void Start()
    {
    }
    float lineWidth = 0.5f;
    RaycastHit rh;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit rh;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out rh);
            if (object1 == null)
            {
                object1 = rh.transform.gameObject;
            }
            else
            if (object2 == null)
            {
                if (object1 != rh.transform.gameObject)
                {
                    object2 = rh.transform.gameObject;
                }
            }
            else
            {
              
                object1 = rh.transform.gameObject;
                object2 = null;
            }
        }
        if (object1 != null && object2 != null)
        {
            if (object1.GetComponent<LineRenderer>() == null)
            {
                object1.AddComponent<LineRenderer>();
            }
            else
            {
                object1.GetComponent<LineRenderer>().startColor =
                Color.green;
                object1.GetComponent<LineRenderer>().endColor =
                Color.green;
                object1.GetComponent<LineRenderer>().positionCount = 2;
                object1.GetComponent<LineRenderer>().SetPosition(0, object1.transform.position);
                object1.GetComponent<LineRenderer>().SetPosition(1, object2.transform.position);
                object1.GetComponent<LineRenderer>().startWidth = lineWidth;
                object1.GetComponent<LineRenderer>().endWidth = lineWidth;
            }
        }
    }
}