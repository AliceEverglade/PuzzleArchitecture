using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for handling the Piece drag movement.
/// </summary>

public class PieceDragging : MonoBehaviour
{
    private Vector3 mouseOffset;
    private float mouseZ;

    private void OnMouseDown()
    {
        getMouseOffset();
    }

    private void getMouseOffset()
    {
        //Get current mouse position and offset
        mouseZ = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mouseOffset = transform.parent.gameObject.transform.position - GetMouseWorldPosition(); //Prevents piece from centering around the player's mouse.
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = mouseZ;

        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

    private void OnMouseDrag()
    {
        transform.parent.gameObject.transform.position = GetMouseWorldPosition() + mouseOffset;
    }
}
