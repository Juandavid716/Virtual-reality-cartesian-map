using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public sealed class Player :MonoBehaviour
{
	public Camera Camera;
	public Transform Plane;
	public Transform Point;
	public Line OriginalLine;
	public Line OriginalAxisLine;
	public GameObject OriginalDot;
	public TextMeshPro OriginalText;

    public GameObject cursorPrefab;
    public GameObject cursorPrefabGray;
    public float maxCursorDistance = 30;
    private GameObject cursorInstance;
    private GameObject cursorInstanceD;
    int contador = 0;
    int impar = 1;
    int indice = 0;
	// TODO: This is unused
	[NonSerialized] public List<Point1> Points;
    [NonSerialized] public  List<Line> Lines;
     Transform mano;
    Transform mano1;

     void Start()
    {
        mano = GameObject.Find("LeftHandAnchor").transform;
        mano1 = GameObject.Find("RightHandAnchor").transform;
        cursorInstance = Instantiate(cursorPrefab);
        cursorInstanceD = Instantiate(cursorPrefabGray);
    }
    void Awake()
	{
		Points = new List<Point1>();
        Lines = new List<Line>(); ;
    }

	void Update()
	{
        UpdateCursor(mano,cursorInstance);
        UpdateCursor1(mano1, cursorInstanceD);
        var mouse = Input.mousePosition;
        //var ray = Camera.ScreenPointToRay(mouse, Camera.MonoOrStereoscopicEye.Mono);
        var ray = new Ray(mano.position, mano.forward);
        var position = Raycast(Plane.transform, ray.origin, ray.direction);
		Point.position = Plane.transform.TransformPoint(position);

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) && (contador==0 ||(contador % 2==0)))

        {
             ray = new Ray(mano.position, mano.forward);
             position = Raycast(Plane.transform, ray.origin, ray.direction);
            Point.position = Plane.transform.TransformPoint(position);
            UpdateCursor(mano, cursorInstance);
            UpdateCursor1(mano1, cursorInstanceD);
            //mano = GameObject.Find("RightHandAnchor").transform;
            foreach (var child in cursorInstanceD.GetComponentsInChildren<Renderer>())
            {
                child.material.color = Color.red;

            }
            foreach (var child in cursorInstance.GetComponentsInChildren<Renderer>())
            {
                child.material.color = Color.gray;

            }
            var point = new Point1
			{
				Position = position,
				Dot = Instantiate(OriginalDot, Plane, false),
				AxisX = Instantiate(OriginalAxisLine, Plane, false),
				AxisY = Instantiate(OriginalAxisLine, Plane, false),
				Text = Instantiate(OriginalText, Plane, false),
			};
			point.Dot.transform.localPosition = position;
			point.AxisX.BuildMesh(new Vector3(position.x, 0, 0), position);
			point.AxisY.BuildMesh(new Vector3(0, position.y, 0), position);
			point.Text.text = $"({position.x:0.0}, {position.y:0.0})";
			point.Text.transform.localPosition = position + new Vector2(1, 1);

			var index = 0;
			//for (; index < Points.Count; index++)
			//{
			//	if (Points[index].Position.x > position.x)
			//	{
			//		Points.Insert(index, point);
			//		break;
			//	}
			//}

			//if (index >= Points.Count)

				Points.Add(point);
            contador++;
            if (contador % 2==0) {
                for (var i = impar; i < Points.Count; i++)
                {

                    var j = i - 1;
                    if (indice >= Lines.Count)

                        Lines.Add(Instantiate(OriginalLine, Plane, false));

                    var line = Lines[indice];
                    var from = Points[j].Position;
                    var to = Points[i].Position;
                    line.BuildMesh(from, to);
                    line.gameObject.SetActive(true);
                }
                Debug.Log(Lines.Count);
                for (var i = Points.Count; i < Lines.Count; i++)
                    Lines[i].gameObject.SetActive(false); //debug
                impar = impar + 2;
                indice++;
            }
			
		} else if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger)&&  (contador % 2 != 0))
        {
            UpdateCursor(mano, cursorInstance);
            UpdateCursor1(mano1, cursorInstanceD);
            ray = new Ray(mano1.position, mano1.forward);
            position = Raycast(Plane.transform, ray.origin, ray.direction);
            Point.position = Plane.transform.TransformPoint(position);
            //mano = GameObject.Find("LeftHandAnchor").transform;
            var point = new Point1
            {
                Position = position,
                Dot = Instantiate(OriginalDot, Plane, false),
                AxisX = Instantiate(OriginalAxisLine, Plane, false),
                AxisY = Instantiate(OriginalAxisLine, Plane, false),
                Text = Instantiate(OriginalText, Plane, false),
            };
            point.Dot.transform.localPosition = position;
            point.AxisX.BuildMesh(new Vector3(position.x, 0, 0), position);
            point.AxisY.BuildMesh(new Vector3(0, position.y, 0), position);
            point.Text.text = $"({position.x:0.0}, {position.y:0.0})";
            point.Text.transform.localPosition = position + new Vector2(1, 1);
            foreach (var child in cursorInstanceD.GetComponentsInChildren<Renderer>())
            {
                child.material.color = Color.gray;

            }
            foreach (var child in cursorInstance.GetComponentsInChildren<Renderer>())
            {
                child.material.color = Color.red;

            }
            var index = 0;
            //for (; index < Points.Count; index++)
            //{
            //	if (Points[index].Position.x > position.x)
            //	{
            //		Points.Insert(index, point);
            //		break;
            //	}
            //}

            //if (index >= Points.Count)

            Points.Add(point);
            contador++;
            if (contador % 2 == 0)
            {
                for (var i = impar; i < Points.Count; i++)
                {

                    var j = i - 1;
                    if (indice >= Lines.Count)

                        Lines.Add(Instantiate(OriginalLine, Plane, false));

                    var line = Lines[indice];
                    var from = Points[j].Position;
                    var to = Points[i].Position;
                    line.BuildMesh(from, to);
                    line.gameObject.SetActive(true);
                }

                for (var i = Points.Count; i < Lines.Count; i++)
                    Lines[i].gameObject.SetActive(false);
                impar = impar + 2;
                indice++;

            }

        }
        if (OVRInput.GetDown(OVRInput.Button.One) || OVRInput.GetDown(OVRInput.Button.Three))
        {
            foreach (var item in Lines)
            {
                DestroyImmediate(item);
            }
            foreach (Point1 item in Points)
            {
                DestroyImmediate(item.Dot);
                DestroyImmediate(item.Text);
                DestroyImmediate(item.AxisX);
                DestroyImmediate(item.AxisY);
            }

            Lines.Clear();
            Points.Clear();
          
            contador = 0;
            impar = 1;
            indice = 0;
            foreach (var child in cursorInstanceD.GetComponentsInChildren<Renderer>())
            {
                child.material.color = Color.gray;

            }
            foreach (var child in cursorInstance.GetComponentsInChildren<Renderer>())
            {
                child.material.color = Color.red;

            }
        }
    }
           
    private void UpdateCursor(Transform manoinstantiate, GameObject cursorInstancia)
    {
    
        Ray ray = new Ray(manoinstantiate.position, manoinstantiate.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            // If the ray hits something, set the position to the hit point
            // and rotate based on the normal vector of the hit
            cursorInstancia.transform.position = hit.point;
            cursorInstancia.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
        }
        else
        {
            // If the ray doesn't hit anything, set the position to the maxCursorDistance
            // and rotate to point away from the camera
            cursorInstancia.transform.position = ray.origin + ray.direction.normalized * maxCursorDistance;
            cursorInstancia.transform.rotation = Quaternion.FromToRotation(Vector3.up, -ray.direction);
        }
    }
    private void UpdateCursor1(Transform manoinstantiate, GameObject cursorInstancia)
    {

        Ray ray = new Ray(manoinstantiate.position, manoinstantiate.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            // If the ray hits something, set the position to the hit point
            // and rotate based on the normal vector of the hit
            cursorInstancia.transform.position = hit.point;
            cursorInstancia.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
        }
        else
        {
            // If the ray doesn't hit anything, set the position to the maxCursorDistance
            // and rotate to point away from the camera
            cursorInstancia.transform.position = ray.origin + ray.direction.normalized * maxCursorDistance;
            cursorInstancia.transform.rotation = Quaternion.FromToRotation(Vector3.up, -ray.direction);
        }
    }


    public static Vector2 Raycast(
		Transform transform,
		Vector3 origin,
		Vector3 direction)
	{
		var localOrigin = transform.InverseTransformPoint(origin);
		var localDirection = transform.InverseTransformDirection(direction);
		var mul = localOrigin.z / localDirection.z;
		return new Vector2(
			localOrigin.x - localDirection.x * mul,
			localOrigin.y - localDirection.y * mul);
	}
}