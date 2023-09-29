using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSnapping : MonoBehaviour
{
    [SerializeField] public bool isSnapped = false;

    private void OnTriggerStay(Collider otherPiece)
    {
        if (!Input.GetMouseButton(0))
        {
            Debug.Log("Yoooooooo");


            if (otherPiece.gameObject.CompareTag("ConnectionPoint"))
            {
                Vector3 destinationPoint = gameObject.transform.position - otherPiece.transform.position;   
                transform.parent.gameObject.transform.position -= destinationPoint;
                isSnapped = true;
            }
        }
        
    }
}
