using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class PieceConnect : MonoBehaviour
{
    [SerializeField] private ConnectionSO connectionSO;
    [SerializeField] private ConnectionManager connectionManager;
    public GameObject connectedConnector = null;

    public static event Action<bool> ToggleConnectUI;

    private void Start()
    {
        connectionManager = FindObjectOfType<ConnectionManager>();
    }

    private void OnTriggerStay(Collider otherPiece)
    {
        // Activate Popup UI
        if (otherPiece.gameObject.CompareTag("ConnectionPoint") && !connectionManager.IsConnected(gameObject, otherPiece.gameObject))
        {
            ToggleConnectUI?.Invoke(true);

            if (Input.GetKey(KeyCode.F))
            {
                connectionSO.Connect(gameObject, otherPiece.gameObject);
                connectedConnector = otherPiece.gameObject;
                otherPiece.GetComponent<PieceConnect>().connectedConnector = gameObject;

                if (gameObject.transform.parent.GetComponent<PuzzlePiece>().selected)
                {
                    connectionManager.ChangeMainPiece(gameObject);
                }

                ToggleConnectUI?.Invoke(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ToggleConnectUI?.Invoke(false);
    }
}