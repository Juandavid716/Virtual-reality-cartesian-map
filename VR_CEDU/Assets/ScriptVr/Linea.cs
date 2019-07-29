using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Camera))]
public class Linea : MonoBehaviour {
    private new Camera camara;
    public Material LineaMaterial;
    public float ancho;
    public float profundidad = 5;
    private Vector3? PuntoInicio = null;
	// Use this for initialization
	void Start () {
        camara = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
        {
            PuntoInicio = GetMouseCameraPoint();

        }

        if(Input.GetMouseButtonUp(0))
        {
            if (!PuntoInicio.HasValue) {
                return;
            }
            var Puntofinal = GetMouseCameraPoint();
            var gameObject = new GameObject();
            var lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.material = LineaMaterial;
            lineRenderer.SetPositions(new Vector3[] { PuntoInicio.Value, Puntofinal.Value });
            lineRenderer.startWidth = ancho;
            lineRenderer.endWidth = ancho;
            PuntoInicio = null;
        }
	}
    private Vector3? GetMouseCameraPoint()

    {
        var ray = camara.ScreenPointToRay(Input.mousePosition);
        return ray.origin + ray.direction * profundidad;
    }
}
