using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;

/// <summary>
/// Responsible for spawning connectors during puzzle CREATION process.
/// </summary>

public class ConnectorSpawner : MonoBehaviour
{
    [SerializeField] private Bounds bounds = new Bounds();
    [SerializeField] private GameObject connectorPrefab;

    //Keeps track of the Face a connector needs to spawn on
    enum Face
    {
        Top,
        Bottom,
        Left,
        Right,
        Front,
        Back
    }

    //Keeps track of which way to offset the connectorpoint horizontally
    enum XPosition
    {
        Left,
        Center,
        Right
    }

    //Keeps track of which way to offset the connectorpoint vertically
    enum YPosition
    {
        Up,
        Center,
        Down
    }

    //Creates a connector
    [Button]
    void SpawnConnector(Face face = Face.Front, XPosition xPosition = XPosition.Center, YPosition yPosition = YPosition.Center)
    {
        GameObject connector = Instantiate(connectorPrefab);

        int pieceID = 1;

        //Checks if connector is being spawned from Piece or SubElement
        if (transform.parent.CompareTag("PieceContainer"))
        {
            pieceID = transform.parent.GetComponent<PieceData>().ID;
            connector.transform.parent = gameObject.transform.parent.transform;
        }

        else if (transform.CompareTag("PieceContainer"))
        {
            pieceID = GetComponent<PieceData>().ID;
            connector.transform.parent = gameObject.transform;
        }

        connector.transform.position = gameObject.transform.position;

        SetConnectorFace(connector, face);
        SetConnectorPosition(connector, xPosition, yPosition);

        connector.name = "P" + pieceID + "C" + DetermineConnectorID(pieceID).ToString();
    }

    //Spawns connector at selected Face
    void SetConnectorFace(GameObject connector, Face face)
    {
        connector.transform.position = gameObject.transform.position;
        bounds = gameObject.GetComponent<BoxCollider>().bounds;

        switch (face)
        {
            case Face.Top:
                connector.transform.position += transform.up * bounds.extents.y;
                //connector.transform.rotation = Quaternion.LookRotation(Vector3.up);
                break;
            case Face.Bottom:
                connector.transform.position += -transform.up * bounds.extents.y;
                //connector.transform.rotation = Quaternion.LookRotation(Vector3.down);
                break;
            case Face.Left:
                connector.transform.position += -transform.right * bounds.extents.z;
                //connector.transform.rotation = Quaternion.LookRotation(Vector3.left);
                break;
            case Face.Right:
                connector.transform.position += transform.right * bounds.extents.z;
                //connector.transform.rotation = Quaternion.LookRotation(Vector3.right);
                break;
            case Face.Front:
                connector.transform.position += transform.forward * bounds.extents.z;
                //connector.transform.rotation = Quaternion.LookRotation(Vector3.forward);
                break;
            case Face.Back:
                connector.transform.position += -transform.forward * bounds.extents.z;
                //connector.transform.rotation = Quaternion.LookRotation(Vector3.back);
                break;
            default:
                break;
        }
    }

    //Sets connector to selected x and y positions.
    void SetConnectorPosition(GameObject connector, XPosition xPosition, YPosition yPosition)
    {
        switch (xPosition)
        {
            case XPosition.Left:
                connector.transform.position += transform.right * -bounds.extents.x;
                break;
            case XPosition.Right:
                connector.transform.position += transform.right * bounds.extents.x;
                break;
            default:
                break;
        }

        switch (yPosition)
        {
            case YPosition.Up:
                connector.transform.position += transform.up * -bounds.extents.y;
                break;
            case YPosition.Down:
                connector.transform.position += transform.right * bounds.extents.y;
                break;
            default:
                break;
        }
    }

    //Calculates which ConnectorID the connector gets.
    int DetermineConnectorID(int pieceID)
    {
        bool unnamed = true;
        int currentConnector = 1;
        while (unnamed)
        {
            if (GameObject.Find("P" + pieceID + "C" + currentConnector) == null)
            {
                unnamed = false;
            }
            else { currentConnector++; }
        }

        return currentConnector;
    }
}