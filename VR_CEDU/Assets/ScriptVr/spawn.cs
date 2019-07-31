using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour {
    public GameObject vertice;
    [SerializeField]
    private OVRInput.Controller m_controller;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            Vector3 position = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
            Quaternion rotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch);
            Instantiate(vertice, position, rotation);
        }
	}
}
