using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PieceConnect : MonoBehaviour
{
    [SerializeField] private ConnectionSO connectionSO;
    [SerializeField] private ConnectionManager connectionManager;
    [SerializeField] private GameObject snappedObject;

    private void Start()
    {
        connectionManager = FindObjectOfType<ConnectionManager>();
    }

    private void OnTriggerStay(Collider otherPiece)
    {
        // Activate Popup UI
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (otherPiece.gameObject.CompareTag("ConnectionPoint") && !connectionManager.IsConnected(gameObject, otherPiece.gameObject))
            {
                connectionSO.Connect(gameObject, otherPiece.gameObject);
                snappedObject = otherPiece.gameObject;
            }
        }
    }

    private void Update()
    {
        Debug.Log("now it got here");
        connectionSO.Disconnect(gameObject, snappedObject);   
    }
}