using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public sealed class Player : MonoBehaviour
{
	public Camera Camera;
	public Transform Plane;
	public Transform Point;
	public Line OriginalLine;
	public GameObject OriginalDot;
	public TextMeshPro OriginalText;

	// TODO: This is unused
	[NonSerialized] public List<Point> Points;

	void Awake()
	{
		Points = new List<Point>();
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
				Dot = Instantiate(OriginalDot, Plane, false),
				AxisX = Instantiate(OriginalLine, Plane, false),
				AxisY = Instantiate(OriginalLine, Plane, false),
				Text = Instantiate(OriginalText, Plane, false),
			};
			point.Dot.transform.localPosition = position;
			point.AxisX.BuildMesh(new Vector3(position.x, 0, 0), position);
			point.AxisY.BuildMesh(new Vector3(0, position.y, 0), position);
			point.Text.text = $"({position.x:0.0}, {position.y:0.0})";
			point.Text.transform.localPosition = position + new Vector2(1, 1);

			Points.Add(point);
            
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