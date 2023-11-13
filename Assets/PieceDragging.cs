using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceDragging : MonoBehaviour
{
    private Vector3 mouseOffset;
    private float mouseZ;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        getMouseOffset();
    }

    private void getMouseOffset()
    {
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
        transform.parent.gameObject.transform.position = GetMouseWorldPosition() + mouseOffset;
    }
}
