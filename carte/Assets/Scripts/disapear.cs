using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disapear : MonoBehaviour {
    public GameObject allfloors;
    public int floornum;
    public int maxfloors;
    private Transform[] VectoresHijos = new Transform[5];
    public CharacterController personaje;
    private Vector3 moveDirection = Vector3.zero;
    // Use this for initialization
    void Start () {
        allchildfloor(allfloors);
        maxfloors = allfloors.transform.childCount-1;
        floornum = 1;
    }

    // Update is called once per frame
    void Update () {
        if (OVRInput.GetDown(OVRInput.Button.Two)) // get up
        {
            Movement(floornum, 5);

        }
        else if (OVRInput.GetDown(OVRInput.Button.One)) // get down
        {
            Movement(floornum, 6);
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
    
     void Movement(int floor, int accion)
    {
        
            if (floor != maxfloors && accion==5)
            {
                floornum = floornum + 1;
                
            //personaje.transform.position = new Vector3(0, 10, 0);
                personaje.enabled = false;
                personaje.transform.position = new Vector3(personaje.transform.position.x, VectoresHijos[floornum].transform.position.y + 1f, personaje.transform.position.z);
                personaje.enabled = true;
            }
            
            else if (floor != 0 && accion == 6)
            {
                floornum = floornum - 1;
                //personaje.transform.position = new Vector3(0, 10, 0);
                personaje.enabled = false;
                personaje.transform.position = new Vector3(personaje.transform.position.x, VectoresHijos[floornum].transform.position.y + 1.8f, personaje.transform.position.z);
                personaje.enabled = true;
            }
       
    }

    void allchildfloor(GameObject allf)
    {
        for (int i = 0; i < VectoresHijos.Length; i++)
        {
            VectoresHijos[i] = null;
        }
       
        for (int i = 0; i < allf.transform.childCount; i++)
        {
            VectoresHijos[i] = allf.transform.GetChild(i);
            //disappearPlane(VectoresHijos[i].gameObject);
            Debug.Log("el piso es " + VectoresHijos[i].transform.position.y);
        }
        
    }
}
