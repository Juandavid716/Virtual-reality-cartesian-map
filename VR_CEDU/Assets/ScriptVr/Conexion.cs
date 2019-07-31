using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conexion : MonoBehaviour {
  
	// Use this for initialization
	void Start () {
		
	}
    GameObject object1;
    GameObject object2;
    // Update is called once per frame
    void Update () {
    
if (Input.GetMouseButtonDown(0))
{
        
            RaycastHit rh;
            
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    

if (Physics.Raycast(ray, out rh, 100))
{
    
object1 = rh.transform.gameObject;
                Debug.Log("entro");
}else

if (object2 == null)
{
if (object1!= rh.transform.gameObject)
{
object2 = rh.transform.gameObject;
}
}
else
{
Destroy(object1.GetComponent<LineRenderer>());
Destroy(object2.GetComponent<LineRenderer>());
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
                Debug.Log("object selected");
                object1.GetComponent<LineRenderer>().startColor = Color.green;
object1.GetComponent<LineRenderer>().endColor = Color.green;
object1.GetComponent<LineRenderer>().positionCount = 2;
object1.GetComponent<LineRenderer>().SetPosition(0, object1.transform.position);
object1.GetComponent<LineRenderer>().SetPosition(1, object2.transform.position);
object1.GetComponent<LineRenderer>().startWidth = 0.5f;
object1.GetComponent<LineRenderer>().endWidth = 0.5f;
}

}

	}
}
