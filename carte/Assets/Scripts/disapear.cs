using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disapear : MonoBehaviour {
    public GameObject allfloors;
    public int floornum = 0;
    private Transform[] VectoresHijos = new Transform[5];
    public CharacterController personaje;
    private Vector3 moveDirection = Vector3.zero;
    // Use this for initialization
    void Start () {
        //allchildfloor(allfloors);

    }

    // Update is called once per frame
    void Update () {
        if (OVRInput.GetDown(OVRInput.Button.Three))
        {
            Movement(0);
        }
       
	}

    void disappearPlane(GameObject ParentP)
    {
        for (int i = 0; i < ParentP.transform.childCount; i++)
        {
            var childplane = ParentP.transform.GetChild(i);
            childplane.GetComponent<Renderer>().enabled = false;
        }

    }

     void Movement(int floor)
    {
        if (floor == 0)
        {
            //personaje.transform.position = new Vector3(0, 10, 0);
        } else if (floor==0 && OVRInput.GetDown(OVRInput.Button.Four))
        {
            Debug.Log("sirvio" + personaje.transform);
        }
    }

    void allchildfloor(GameObject allf)
    {
        for (int i = 0; i < VectoresHijos.Length; i++)
        {
            VectoresHijos[i] = null;
        }
        Debug.Log(allf.transform.childCount);
        for (int i = 0; i < allf.transform.childCount; i++)
        {
            VectoresHijos[i] = allf.transform.GetChild(i);
        }
    }
}
