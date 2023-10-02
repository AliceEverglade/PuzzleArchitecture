using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PieceConnect : MonoBehaviour
{
    [SerializeField] private ConnectionSO connectionSO;
    [SerializeField] private ConnectionManager connectionManager;
    public GameObject snappedObject;

    private void Start()
    {
        connectionManager = FindObjectOfType<ConnectionManager>();
    }

    private void OnTriggerStay(Collider otherPiece)
    {   
        // Activate Popup UI
        if (Input.GetKey(KeyCode.F))
        {
            if (otherPiece.gameObject.CompareTag("ConnectionPoint") && !connectionManager.IsConnected(gameObject, otherPiece.gameObject) && gameObject.transform.parent.GetComponent<PuzzlePiece>().selected)
            {
                connectionSO.Connect(gameObject, otherPiece.gameObject);
                snappedObject = otherPiece.gameObject;
                otherPiece.GetComponent<PieceConnect>().snappedObject = gameObject;
            }
        }
    }
}