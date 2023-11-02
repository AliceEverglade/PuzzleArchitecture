using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PieceDrag : MonoBehaviour
{
    private Vector3 mouseOffset;
    private float mouseZ;
    private bool snapped = false;
    private bool canSnap = false;

    void OnEnable()
    {
        UIManager.OnUIChange += ToggleSnapability;
    }

    void OnDisable()
    {
        UIManager.OnUIChange -= ToggleSnapability;
    }

    private void OnMouseDown()
    {
        getMouseOffset();
    }

    private void getMouseOffset() {
        mouseZ = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mouseOffset = transform.parent.gameObject.transform.position - GetMouseWorldPosition();
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = mouseZ;

        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

    private void OnMouseDrag()
    {
        if (Input.GetKey(KeyCode.F) && canSnap) //check if you CAN snap
        {
            snapped = true;
        }
        
        if (!snapped)
        {
            transform.parent.gameObject.transform.position = GetMouseWorldPosition() + mouseOffset;
        }
    }

    private void ToggleSnapability(bool toggle)
    {
        canSnap = toggle;
    }

    private void OnMouseUp()
    {
        snapped = false;
    }
}
