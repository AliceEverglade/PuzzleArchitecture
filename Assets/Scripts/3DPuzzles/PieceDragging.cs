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
    private bool canConnect => transform.parent.GetComponent<PieceHandler>().CanConnect;
    //private bool canDrag = true;

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
        //if (Input.GetKey(KeyCode.F) && canConnect)
        //{
        //    canDrag = false;
        //}

        //if (canDrag)
        //{
            transform.parent.gameObject.transform.position = GetMouseWorldPosition() + mouseOffset;
        //}
    }

    private void OnMouseUp()
    {
        //canDrag = true;
    }
}
