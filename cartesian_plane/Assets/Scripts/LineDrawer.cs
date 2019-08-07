using System.Collections.Generic;
using UnityEngine;
 
public struct LineTargets
{
    public Transform start, end;
 
    public LineTargets(Transform start, Transform end)
    {
        this.start = start;
        this.end = end;
    }
}
 
public class LineDrawer : MonoBehaviour
{
    public Transform controller1, controller2;
 
    public Color startColour = Color.green, endColour = Color.red;
    public float startWidth = 1, endWidth = 1;
 
    private List<LineRenderer> lines = new List<LineRenderer>();
    private List<LineTargets> targets = new List<LineTargets>();
 
    private Transform tempTarget;
 
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            AssignTransform(controller1);
        }
        if (Input.GetKeyDown(KeyCode.Mouse1)) {
            AssignTransform(controller2);
        }
    }
    private void Update()
    {
        // Creating LineRenderers for each Target Group
        while(lines.Count < targets.Count) {
            GameObject newObject = new GameObject("Line: " + lines.Count);
            LineRenderer newLine = newObject.AddComponent<LineRenderer>();
 
            newLine.positionCount = 2;
 
            newLine.startColor = startColour;
            newLine.endColor = endColour;
 
            newLine.startWidth = startWidth;
            newLine.endWidth = endWidth;
 
            lines.Add(newLine);
        }
 
        // Updating Line Positions
        for(int i = 0; i < lines.Count; i++) {
            lines[i].SetPosition(0, targets[i].start.transform.position);
            lines[i].SetPosition(1, targets[i].end.transform.position);
        }
    }
 
    private void AssignTransform(Transform controller)
    {
        RaycastHit hit;
 
        if(Physics.Raycast(controller.position, controller.forward, out hit)) {
 
            // Deselecting Object for Temp1
            if(hit.transform.gameObject == tempTarget) {
                tempTarget = null;
                return;
            }
 
            // Adding new GameObject
            if(tempTarget == null) {
                tempTarget = hit.transform;
            }
 
            // Creating new Target
            else {
                targets.Add(new LineTargets(tempTarget, hit.transform));
            }
        }
    }
}