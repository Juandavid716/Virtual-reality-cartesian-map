using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public sealed class Player: MonoBehaviour
{
	public Camera Camera;
	public Transform Plane;
	public Transform Point;
	public Line OriginalLine;
	public Line OriginalAxisLine;
	public GameObject OriginalDot;
	public TextMeshPro OriginalText;
    int contador = 0;
    int impar = 1;
    int indice = 0;
	// TODO: This is unused
	[NonSerialized] public List<Point> Points;
	[NonSerialized] public List<Line> Lines;
   
    void Awake()
	{
		Points = new List<Point>();
		Lines = new List<Line>();
	}

	void Update()
	{
		var mouse = Input.mousePosition;
		var ray = Camera.ScreenPointToRay(mouse, Camera.MonoOrStereoscopicEye.Mono);
		var position = Raycast(Plane.transform, ray.origin, ray.direction);
		Point.position = Plane.transform.TransformPoint(position);

		if (Input.GetMouseButtonDown(0))
		{
			var point = new Point
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

                for (var i = Points.Count; i < Lines.Count; i++)
                    Lines[i].gameObject.SetActive(false);
                impar = impar + 2;
                indice++;
            }
			
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