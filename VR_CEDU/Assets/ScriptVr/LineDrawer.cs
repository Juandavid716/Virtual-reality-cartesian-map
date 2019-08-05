using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineDrawer : MonoBehaviour
{
    #region Data
    public Transform controller1, controller2;

    public Color startColour = Color.green, endColour = Color.red;
    public float startWidth = 1, endWidth = 1;

    private LineRenderer lineRenderer;
    private GameObject obj1, obj2;
    #endregion

    #region Unity Functions
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;

        lineRenderer.startColor = startColour;
        lineRenderer.endColor = endColour;

        lineRenderer.startWidth = startWidth;
        lineRenderer.endWidth = endWidth;
    }
    private void Update()
    {
        if (obj1 != null)
            lineRenderer.SetPosition(0, obj1.transform.position);

        if (obj2 != null)
            lineRenderer.SetPosition(1, obj2.transform.position);
    }
    RaycastHit hit1;
    RaycastHit hit2;
    private void FixedUpdate()
    {
        if (Physics.Raycast(controller1.position, controller1.forward, out hit1))
            obj1 = hit1.collider.gameObject;

        if (Physics.Raycast(controller2.position, controller2.forward, out  hit2))
            obj2 = hit2.collider.gameObject;
    }
    #endregion
}