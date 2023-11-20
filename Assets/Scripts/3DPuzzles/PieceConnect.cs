using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

/// <summary>
/// script on connection points that handles connecting and disconnecting
/// </summary>
public class PieceConnect : MonoBehaviour
{
    [SerializeField] private ConnectionSO connectionSO;
    [SerializeField] private ConnectionManager connectionManager;
    [SerializeField] private PieceData pieceData;

    public GameObject connectedConnector = null;

    //toggles UI to signal connecting being available
    public static event Action<bool, string, string, Color?> ToggleConnectUI;

    private void Start()
    {
        pieceData = gameObject.transform.parent.GetComponent<PieceData>();
        connectionManager = FindObjectOfType<ConnectionManager>();
    }

    private void Update()
    {
        Disconnect();
    }

    private void OnTriggerStay(Collider otherPiece)
    {
        // Activate Popup UI
        if (otherPiece.gameObject.CompareTag("ConnectionPoint") && !connectionManager.IsConnected(gameObject, otherPiece.gameObject))
        {
            //ToggleConnectUI?.Invoke(true, "ConnectUI", null, null);
            
            Connect(otherPiece);
        }
    }

    //connects connection points to eachother.
    private void Connect(Collider otherPiece)
    {
        if (Input.GetKey(KeyCode.F))
        {
            connectionSO.Connect(gameObject, otherPiece.gameObject);
            connectedConnector = otherPiece.gameObject;
            otherPiece.GetComponent<PieceConnect>().connectedConnector = gameObject;

            if (gameObject.transform.parent.GetComponent<PieceData>().Selected)
            {
                connectionManager.ChangeMainPiece(gameObject.transform.parent.gameObject);
            }

            ToggleConnectUI?.Invoke(false, "ConnectUI", null, null);
        }
    }

    //breaks a connection.
    private void Disconnect()
    {
        if (Input.GetKeyDown(KeyCode.G) && pieceData.Selected)
        {
            foreach (GameObject connector in pieceData.Connectors)
            {

                GameObject secondConnector = connector.GetComponent<PieceConnect>().connectedConnector;

                connectionSO.Disconnect(connector, secondConnector);

                if (connector.GetComponent<PieceConnect>().connectedConnector != null)
                {
                    secondConnector.GetComponent<PieceConnect>().connectedConnector = null;
                    connector.GetComponent<PieceConnect>().connectedConnector = null;
                }
            }
        }
    }

    //turns off UI
    private void OnTriggerExit(Collider other)
    {
        ToggleConnectUI?.Invoke(false, "ConnectUI", null, null);
    }
}