using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour {
    private int count = 5;
    public GameObject spawnObject;
    public Collider playerControl;
    public GameObject ParentPlane;

	// Use this for initialization
	void Start () {
        for (int i = -5; i < count; i++)
        {
            
            for (int j = -5; j < count; j++)
            {
               
                for (int k = -5; k < count; k++)
                {
                    Instantiate(spawnObject, new Vector3(i, j, k), new Quaternion(0, 0, 0, 0));
                    spawnObject.GetComponent<Renderer>().enabled = false;

                }
            }
        }

       
      
    }

    
    // Update is called once per frame
    void Update () {
    
	}
  
}
