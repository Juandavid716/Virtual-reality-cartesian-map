using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public sealed class Player : MonoBehaviour
{
    public Camera Camera;
    public Transform Plane;
    public Transform Punto;
    public Line OriginalLine;
    public GameObject OriginalDot;
    public TextMeshPro OriginalText;

    // TODO: This is unused
    [NonSerialized] public List<Punto> Points;

    void Awake()
    {
        Points = new List<Punto>();
    }

    void Update()
    {
        var mouse = Input.mousePosition;
        var ray = Camera.ScreenPointToRay(mouse, Camera.MonoOrStereoscopicEye.Mono);
        var position = Raycast(Plane.transform, ray.origin, ray.direction);
        Punto.position = Plane.transform.TransformPoint(position);

        if (Input.GetMouseButtonDown(0))
        {
            var point = new Punto
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

public sealed class Line : MonoBehaviour
{
    static readonly int[] Triangles =
    {
        2, 1, 0,
        2, 3, 1,
    };

    static readonly Vector2[] Uvs =
    {
        new Vector2(0, 0),
        new Vector2(1, 0),
        new Vector2(0, 1),
        new Vector2(1, 1),
    };

    public Vector3 Up = new Vector3(0, 1, 0);
    public float Width = 1;

    [NonSerialized] public Vector3[] Vertices;
    [NonSerialized] public MeshFilter Filter;
    [NonSerialized] public Mesh Mesh;

    void OnEnable()
    {
        Vertices = new Vector3[4];

        Filter = GetComponent<MeshFilter>();
        if (Filter == null)
            Filter = gameObject.AddComponent<MeshFilter>();


        Mesh = Filter.mesh;
        if (Mesh == null)
        {
            Mesh = new Mesh
            {
                name = "DirectLine (generated)",
                hideFlags = HideFlags.HideAndDontSave,
                vertices = Vertices,
                uv = Uvs,
                triangles = Triangles,
            };

            Filter.sharedMesh = Mesh;
        }
    }

    void OnDestroy() => Destroy(Mesh);

    public void BuildMesh(Vector3 p0, Vector3 p1)
    {
        // If you be p0 and p1 to be world positions
        // p0 = transform.InverseTransformPoint(p0);

        // Apply lhr to get the offset
        // See lhr as you create a plane with two vectors and the output it the up vector
        // So we create a plane which follows the line
        // Lastly we normalize and scale the vector to have a fixed width
        // I'm not sure if we get the right or the left value, but this doesn't matter
        var delta = p0 - p1;
        var offset = Vector3.Cross(Up, delta);
        offset.Normalize();
        offset *= Width;

        // Fill the vertices
        Vertices[0] = p0 + offset;
        Vertices[1] = p0 - offset;
        Vertices[2] = p1 + offset;
        Vertices[3] = p1 - offset;

        // Apply vertices to mesh
        Mesh.vertices = Vertices;
        Mesh.uv = Uvs;
        Mesh.triangles = Triangles;
        Mesh.RecalculateBounds();
        Mesh.RecalculateNormals();
    }
}


 
public struct Punto
{
    public GameObject Dot;
    public Line AxisX;
    public Line AxisY;
    public TextMeshPro Text;
}