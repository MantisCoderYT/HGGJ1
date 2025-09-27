using UnityEngine;

public class DraggableSnap2D : MonoBehaviour
{
    private Vector3 offset;
    private Camera cam;
    private bool dragging = false;

    [Header("Snapping")]
    public float snapRange = 0.5f;

    private Vector3 startPos;
    private SnapPoint currentSnapPoint;

    void Start()
    {
        cam = Camera.main;
        startPos = transform.position;
    }

    void OnMouseDown()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - new Vector3(mousePos.x, mousePos.y, transform.position.z);
        dragging = true;

        // free up slot if this piece was already snapped
        if (currentSnapPoint != null && currentSnapPoint.occupant == gameObject)
        {
            currentSnapPoint.isOccupied = false;
            currentSnapPoint.occupant = null;
            currentSnapPoint = null;
        }
    }

    void OnMouseDrag()
    {
        if (dragging)
        {
            Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z) + offset;
        }
    }

    void OnMouseUp()
    {
        dragging = false;

        SnapPoint nearest = FindNearestSnapPoint();

        if (nearest != null)
        {
            // ✅ Snap in
            transform.position = nearest.transform.position;
            nearest.isOccupied = true;
            nearest.occupant = gameObject;
            currentSnapPoint = nearest;
        }
        else
        {
            // ❌ No valid snap point, go back
            transform.position = startPos;
        }
    }

    private SnapPoint FindNearestSnapPoint()
    {
        SnapPoint[] allPoints = Object.FindObjectsByType<SnapPoint>(FindObjectsSortMode.None);
        SnapPoint nearest = null;
        float minDist = Mathf.Infinity;

        foreach (SnapPoint point in allPoints)
        {
            if (point.isOccupied) continue; // skip taken slots

            float dist = Vector2.Distance(transform.position, point.transform.position);
            if (dist < minDist && dist <= snapRange)
            {
                minDist = dist;
                nearest = point;
            }
        }

        return nearest;
    }
}
