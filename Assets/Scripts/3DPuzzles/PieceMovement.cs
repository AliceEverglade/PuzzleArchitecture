using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMovement : MonoBehaviour
{
    [SerializeField] Vector3 origin;
    [SerializeField] private Vector3 startPos;

    [Range(0.1f, 20f)]
    [SerializeField] private float sensitivity;

    private void OnMouseDown()
    {
        startPos = Input.mousePosition;
        origin = transform.position;
    }

    private void OnMouseDrag()
    {
        MovePiece();
    }

    void MovePiece()
    {
        Vector3 offset;
        offset = new Vector3(
            (Input.mousePosition.x - startPos.x) / (1000 / sensitivity),
            (Input.mousePosition.y - startPos.y) / (1000 / sensitivity),
            0
        );

        transform.position = origin + offset;
    }
}
