using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSnapping : MonoBehaviour
{
    [SerializeField] public bool isSnapped = false;
    [SerializeField] private PieceMovement pieceMovement;

    // Start is called before the first frame update
    void Start()
    {
        pieceMovement = transform.parent.gameObject.GetComponent<PieceMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider otherPiece)
    {
        if (otherPiece.gameObject.CompareTag("ConnectionPoint") && pieceMovement.isMoving)
        {
            Debug.Log("Yoooooooo");
            Vector3 destinationPoint = gameObject.transform.position - otherPiece.transform.position;
            Debug.Log(string.Format("X: {0}. Y: {1}, Z {2}", destinationPoint.x, destinationPoint.y, destinationPoint.z));
            transform.parent.gameObject.transform.position -= destinationPoint;

            isSnapped = true;
        }
    }
}
