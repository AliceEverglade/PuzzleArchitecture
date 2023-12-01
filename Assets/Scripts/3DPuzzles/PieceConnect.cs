using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

/// <summary>
/// Script on connection points that handles connecting and disconnecting.
/// </summary>

public class PieceConnect : MonoBehaviour
{
    [SerializeField] private ConnectionSO connectionSO;
    [SerializeField] private ConnectionManager connectionManager;
    [SerializeField] private PieceData pieceData;
    [SerializeField] private GameObject audioObject;

    [SerializeField] private AudioClip connectFX;
    [SerializeField] private AudioClip disconnectFX;
    
    public GameObject connectedConnector = null;

    //Toggles UI to signal connecting being available.
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
        //Activate Popup UI
        if (otherPiece.gameObject.CompareTag("ConnectionPoint") && !connectionManager.IsConnected(gameObject, otherPiece.gameObject))
        {
            ToggleConnectUI?.Invoke(true, "ConnectUI", null, null);
            transform.parent.GetComponent<PieceHandler>().CanConnect = true;
            
            Connect(otherPiece);
        }
    }

    //connects connection points to eachother.
    private void Connect(Collider otherPiece)
    {
        if (Input.GetKey(KeyCode.F) && pieceData.Selected)
        {
            connectionSO.Connect(gameObject, otherPiece.gameObject);
            connectedConnector = otherPiece.gameObject;
            otherPiece.GetComponent<PieceConnect>().connectedConnector = gameObject;
            connectionManager.ChangeMainPiece(gameObject.transform.parent.gameObject);
            transform.parent.GetComponent<PieceHandler>().CanConnect = false;
            ToggleConnectUI?.Invoke(false, "ConnectUI", null, null);

            SFXHandler.Instance().PlaySound(audioObject, connectFX);
        }
    }

    //breaks a connection.
    private void Disconnect()
    {
        bool CanDisconnect = false;

        if (Input.GetKeyDown(KeyCode.G) && pieceData.Selected)
        {
            foreach (GameObject connector in pieceData.Connectors)
            {
                GameObject secondConnector = connector.GetComponent<PieceConnect>().connectedConnector;

                connectionSO.Disconnect(connector, secondConnector);

                if (connector.GetComponent<PieceConnect>().connectedConnector != null)
                {
                    CanDisconnect = true;
                    secondConnector.GetComponent<PieceConnect>().connectedConnector = null;
                    connector.GetComponent<PieceConnect>().connectedConnector = null;
                }
            }
        }

        if (CanDisconnect)
        {
            SFXHandler.Instance().PlaySound(audioObject, disconnectFX);
        }
    }

    //turns off UI
    private void OnTriggerExit(Collider other)
    {
        transform.parent.GetComponent<PieceHandler>().CanConnect = false;
        ToggleConnectUI?.Invoke(false, "ConnectUI", null, null);
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, 0.125f);
    }*/
}