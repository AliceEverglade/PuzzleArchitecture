using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMovement : MonoBehaviour
{
    [SerializeField] private Vector3 origin;
    [SerializeField] private Vector3 startPos;

    [Range(0.1f, 20f)]
    [SerializeField] private float sensitivity;

    [SerializeField] public bool isMoving;
    [SerializeField] public bool snapReset;
    [SerializeField] private PieceSnapping pieceSnapping;
    [SerializeField] private Side side;

    enum Side
    {
        Top,
        Bottom,
        Right,
        Left,
        Front,
        Back
    }

    private void Start()
    {
        pieceSnapping = transform.GetChild(0).gameObject.GetComponent<PieceSnapping>();
        snapReset = false;
    }

    private void OnMouseDown()
    {
        startPos = Input.mousePosition;
        origin = transform.position;

        CheckSide();
    }

    private void CheckSide()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                Vector3 normal = hit.normal;

                // Determine which side of the cube was clicked based on hit.normal
                if (normal == Vector3.up)
                {
                    side = Side.Top;
                }
                else if (normal == -Vector3.up)
                {
                    side = Side.Bottom;
                }
                else if (normal == Vector3.right)
                {
                    side = Side.Right;
                }
                else if (normal == -Vector3.right)
                {
                    side = Side.Left;
                }
                else if (normal == Vector3.forward)
                {
                    side = Side.Front;
                }
                else if (normal == -Vector3.forward)
                {
                    side = Side.Bottom;
                }
            }
        }
    }

    private void OnMouseDrag()
    {
        MovePiece();
        isMoving = true;
    }

    private void OnMouseUp()
    {
        isMoving = false;
        snapReset = false;
    }

    void MovePiece()
    {
        Vector3 offset = new Vector3(0, 0, 0);
        float mouseX = (Input.mousePosition.x - startPos.x) / (1000 / sensitivity);
        float mouseY = (Input.mousePosition.y - startPos.y) / (1000 / sensitivity);

        if (!pieceSnapping.isSnapped)
        {
            switch(side)
            {
                case Side.Top:
                    offset = new Vector3(mouseX, 0, mouseY);
                    break;
                default:
                    break;

            }

            transform.position = origin + offset;
        }

        if (pieceSnapping.isSnapped)
        {
            if (snapReset)
            {
                startPos = Input.mousePosition;
                snapReset = false;
            }

            if (Mathf.Abs(Input.mousePosition.x - 10) < startPos.x)
            {
                transform.position += new Vector3(1, 0, 0);
                pieceSnapping.isSnapped = false;
            }
        }
    }
}
