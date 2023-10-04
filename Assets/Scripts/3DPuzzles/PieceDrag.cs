using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceDrag : MonoBehaviour
{
    private Vector3 mouseOffset;
    private float mouseZ;
    private bool snapped = false;

    private void OnMouseDown()
    {
        getMouseOffset();
    }

    private void getMouseOffset() {
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
        if (Input.GetKey(KeyCode.F)) //check if you CAN snap
        {
            snapped = true;
        }
        
        if (!snapped)
        {
            transform.position = GetMouseWorldPosition() + mouseOffset;
        }
    }

    private void OnMouseUp()
    {
        snapped = false;
    }
}
