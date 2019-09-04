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
    public TextMeshPro Text1;
    public TextMeshPro Text2;
    private TextMeshPro m_Text;
    private TextMeshPro m_Text1;
    public GameObject cursorPrefab;
    public GameObject cursorPrefabGray;
    public float maxCursorDistance = 4f;
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
        m_Text = Instantiate(Text1);
        m_Text1 = Instantiate(Text2);
    }
    void Awake()
	{
		Points = new List<Point1>();
        Lines = new List<Line>(); ;
    }

	void Update()
	{

        
        //var ray = Camera.ScreenPointToRay(mouse, Camera.MonoOrStereoscopicEye.Mono);
        //var ray = new Ray(mano.position, mano.forward);
        var rotation = Quaternion.LookRotation(Plane.position - mano.transform.position);
       var rotation1 = Quaternion.LookRotation(Plane.position - mano1.transform.position);
        var matrix = Matrix4x4.TRS(Camera.transform.position, rotation, Vector3.one);
        var matrix1 = Matrix4x4.TRS(Camera.transform.position, rotation1, Vector3.one);
        var ray = new Ray(mano.position, mano.forward);
        var ray1 = new Ray(mano1.position, mano1.forward);
        var position = Raycast(matrix, ray.origin, ray.direction);
        var position1 = Raycast(matrix1, ray1.origin, ray1.direction);
        UpdateCursor(mano, cursorInstance, position);
        UpdateCursor1(mano1, cursorInstanceD, position1);
        //var position = Raycast(Plane.transform, ray.origin, ray.direction);
        Point.position = Plane.transform.TransformPoint(position);

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) && (contador==0 ||(contador % 2==0)))

        {

             rotation = Quaternion.LookRotation(Plane.position - mano.transform.position);

            ray = new Ray(mano.position, mano.forward);
             
             matrix = Matrix4x4.TRS(Camera.transform.position, rotation, Vector3.one);
             position = Raycast(matrix, ray.origin, ray.direction);

            //position = Raycast(Plane.transform, ray.origin, ray.direction);
            Point.position = Plane.transform.TransformPoint(position);
            UpdateCursor(mano, cursorInstance, position);
            UpdateCursor1(mano1, cursorInstanceD, position1);
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
			point.Text.text = $"({position.x:0.0}, {position.y:0.0},{position.z:0.0})";
			point.Text.transform.position = position + new Vector3(1, 1,1);
           
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
             rotation1 = Quaternion.LookRotation(Plane.position - mano1.transform.position);
            ray1 = new Ray(mano1.position, mano1.forward);
    
            matrix1 = Matrix4x4.TRS(Camera.transform.position, rotation1, Vector3.one);
             position1 = Raycast(matrix1, ray1.origin, ray1.direction);
            UpdateCursor(mano, cursorInstance, position);
            UpdateCursor1(mano1, cursorInstanceD, position1);
       
            Point.position = Plane.transform.TransformPoint(position1);
            //mano = GameObject.Find("LeftHandAnchor").transform;
            var point = new Point1
            {
                Position = position1,
                Dot = Instantiate(OriginalDot, Plane, false),
                AxisX = Instantiate(OriginalAxisLine, Plane, false),
                AxisY = Instantiate(OriginalAxisLine, Plane, false),
                Text = Instantiate(OriginalText, Plane, false),
            };
            point.Dot.transform.localPosition = position1;
            point.AxisX.BuildMesh(new Vector3(position1.x, 0, 0), position1);
            point.AxisY.BuildMesh(new Vector3(0, position1.y, 0), position1);
            point.Text.text = $"({position1.x:0.0}, {position1.y:0.0},{position1.z:0.0})";
            point.Text.transform.position = position1 + new Vector3(1, 1,1);
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
           
    private void UpdateCursor(Transform manoinstantiate, GameObject cursorInstancia, Vector3 position)
    {
        m_Text.fontSize = 6;
        
        //m_Text.transform.localPosition = position;
        Ray ray = new Ray(manoinstantiate.position, manoinstantiate.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            // If the ray hits something, set the position to the hit point
            // and rotate based on the normal vector of the hit
            cursorInstancia.transform.position = hit.point;
            m_Text.text = $"({position.x:0.0}, {position.y:0.0},{position.z:0.0})";
            cursorInstancia.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            m_Text.transform.localPosition = hit.point;
            m_Text.transform.localRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
        }
        else
        {
            // If the ray doesn't hit anything, set the position to the maxCursorDistance
            // and rotate to point away from the camera
            cursorInstancia.transform.position = ray.origin + ray.direction.normalized * maxCursorDistance;
            //Debug.Log(new Vector3(position.x,position.y,position.z));
            m_Text.text = $"({position.x:0.0}, {position.y:0.0},{position.z:0.0})";
            cursorInstancia.transform.rotation = Quaternion.FromToRotation(Vector3.up, -ray.direction);
            m_Text.transform.localPosition = ray.origin + ray.direction.normalized * maxCursorDistance;
            m_Text.transform.localRotation = Quaternion.FromToRotation(new Vector3(0,0,0), ray.direction);
        }
    }
    private void UpdateCursor1(Transform manoinstantiate, GameObject cursorInstancia, Vector3 position)
    {
        //m_Text1.fontSize = 12;
        
        //Debug.Log(position);
   
        Ray ray = new Ray(manoinstantiate.position, manoinstantiate.forward);
        RaycastHit hit;
        m_Text1.fontSize = 6;
       
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            // If the ray hits something, set the position to the hit point
            // and rotate based on the normal vector of the hit
            cursorInstancia.transform.position = hit.point;
            //m_Text1.text = $"({position.x:0.0}, {position.y:0.0},{position.z:0.0})";
            cursorInstancia.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

            m_Text1.transform.localPosition = hit.point;
            m_Text1.transform.localRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
        }
        else
        {
            // If the ray doesn't hit anything, set the position to the maxCursorDistance
            // and rotate to point away from the camera
            cursorInstancia.transform.position = ray.origin + ray.direction.normalized * maxCursorDistance;
            cursorInstancia.transform.rotation = Quaternion.FromToRotation(Vector3.up, -ray.direction);
             m_Text1.text = $"({position.x:0.0}, {position.y:0.0},{position.z:0.0})";
            m_Text1.transform.localPosition = ray.origin + ray.direction.normalized * maxCursorDistance;
            m_Text1.transform.localRotation = Quaternion.FromToRotation(new Vector3(0, 0, 0), ray.direction);
        }
    }


    void Foo()
    {
    
   


        // TODO: figure out what the orientation should be
       
        //Debug.Log(position);
       
    }

    public static Vector3 Raycast(
    Matrix4x4 matrix,
    Vector3 origin,
    Vector3 direction)
    {
        var invMatrix = Matrix4x4.Inverse(matrix);
        var localOrigin = invMatrix * origin;
        var localDirection = invMatrix * direction;
        var mul = localOrigin.z / localDirection.z;
        var localPosition = new Vector2(
        localOrigin.x - localDirection.x * mul,
        localOrigin.y - localDirection.y * mul);

        var worldPosition = matrix * localPosition;
        return worldPosition;
        //return transform.InverseTransformPoint(worldPosition);
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