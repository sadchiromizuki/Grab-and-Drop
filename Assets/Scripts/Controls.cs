using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Controls : MonoBehaviour
{
    public GameObject playerPos;
    public Camera cam;
    public DragRange gizmoRange;
    public UI uiUpdate;
    private Vector3 screenPoint;
    Vector3 offset;

    bool isPicked = false;

    public float dist;

    private void Update()
    {
        dist = Vector3.Distance(transform.position, gizmoRange.transform.position);
    }
    private void OnMouseDown()
    {
        isPicked = true;

        if(dist <= gizmoRange.gizmoRange)
        {
            screenPoint = cam.WorldToScreenPoint(gameObject.transform.position);
            offset = gameObject.transform.position - cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        }
    }

    private void OnMouseUp()
    {
        isPicked = false;
    }

    private void OnMouseDrag()
    {
        isPicked = true;
        if (dist <= gizmoRange.gizmoRange)
        {
            Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 cursorPosition = cam.ScreenToWorldPoint(cursorPoint) + offset;
            transform.position = cursorPosition;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, playerPos.transform.position);
    }
}
