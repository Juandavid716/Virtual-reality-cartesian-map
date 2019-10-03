using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disapear : MonoBehaviour {
    public Component[] RendererElements;
    // Use this for initialization
    void Start () {
        RendererElements = GetComponentsInChildren<Renderer>();
        foreach (Renderer x in RendererElements)
            x.enabled = false;

    }

    // Update is called once per frame
    void Update () {
		
	}
}
