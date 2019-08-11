using System;
using UnityEngine;


public sealed class Line : MonoBehaviour
{
    static readonly int[] Triangles =
    {
        2, 1, 0,
        2, 3, 1,
        6, 5, 4,
        6, 7, 5
    };

    static readonly Vector2[] Uvs =
    {
        new Vector2(0, 0),
        new Vector2(1, 0),
        new Vector2(0, 1),
        new Vector2(1, 1),
        new Vector2(0, 0),
        new Vector2(1, 0),
        new Vector2(0, 1),
        new Vector2(1, 1),
    };

    public Vector3 Forward = new Vector3(0, 0, -1);
    public Vector3 Right = new Vector3(-1, 0, 0);
    public float Width = 1;

    [NonSerialized] public Vector3[] Vertices;
    [NonSerialized] public MeshFilter Filter;
    [NonSerialized] public Mesh Mesh;

    void OnEnable()
    {
        Vertices = new Vector3[8];

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
        var delta = p0 - p1;

        var offset0 = Vector3.Cross(Forward, delta);
        offset0.Normalize();
        offset0 *= Width;
        Vertices[0] = p0 + offset0;
        Vertices[1] = p0 - offset0;
        Vertices[2] = p1 + offset0;
        Vertices[3] = p1 - offset0;

        var offset1 = Vector3.Cross(Right, delta);
        offset1.Normalize();
        offset1 *= Width;
        Vertices[4] = p0 + offset1;
        Vertices[5] = p0 - offset1;
        Vertices[6] = p1 + offset1;
        Vertices[7] = p1 - offset1;

        // Apply vertices to mesh
        Mesh.vertices = Vertices;
        Mesh.uv = Uvs;
        Mesh.triangles = Triangles;
        Mesh.RecalculateBounds();
        Mesh.RecalculateNormals();
    }
}