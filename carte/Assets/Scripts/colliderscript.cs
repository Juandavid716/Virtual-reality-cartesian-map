using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliderscript : MonoBehaviour {

    // The size of the array determines how many raycasts will occur
    RaycastHit[] m_Results = new RaycastHit[30];
    public Transform mano;
    void Update()
    {
        // Set the layer mask to all layers
        var layerMask = ~0;

        // Do any of the rays hit?
        if (Physics.RaycastNonAlloc(mano.position, mano.forward, m_Results, Mathf.Infinity, layerMask) > 0)
        {
            foreach (var result in m_Results)
            {
                // Check for null since some array spots might be
                if (result.collider != null)
                {
                    Debug.Log("Hit " + result.collider.gameObject.name);
                }
            }
        }
        else
        {
            Debug.Log("Did not hit");
        }
    }
}
