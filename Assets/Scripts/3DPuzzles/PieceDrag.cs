using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceDrag : MonoBehaviour
{
    private Vector3 mouseOffset;
    private float mouseZ;
    
    private void OnMouseDown()
    {
        mouseZ = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mouseOffset = gameObject.transform.position - GetMouseWorldPosition();
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = mouseZ;

        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPosition() + mouseOffset;
    }
}
